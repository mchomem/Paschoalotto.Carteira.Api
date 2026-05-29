namespace Paschoalotto.Carteira.Core.Application.Services;

public class AgreementService : IAgreementService
{
    private readonly IAcordoRepository _acordoRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IParcelaAcordoRepository _parcelaAcordoRepository;
    private readonly IDebtCalculationService _debtCalculationService;

    public AgreementService(
        IAcordoRepository acordoRepository,
        IContratoRepository contratoRepository,
        IParcelaAcordoRepository parcelaAcordoRepository,
        IDebtCalculationService debtCalculationService)
    {
        _acordoRepository = acordoRepository;
        _contratoRepository = contratoRepository;
        _parcelaAcordoRepository = parcelaAcordoRepository;
        _debtCalculationService = debtCalculationService;
    }

    public async Task<AcordoResponseDto> CreateAcordoAsync(AcordoRequestDto request)
    {
        // Validar se contrato existe
        var contrato = await _contratoRepository.GetWithParcelasAsync(request.ContratoId);
        if (contrato == null)
            throw new ContratoNotFoundException(request.ContratoId);

        // Validar se já existe acordo ativo para o contrato
        var acordoAtivo = await _acordoRepository.GetAcordoAtivoByContratoIdAsync(request.ContratoId);
        if (acordoAtivo != null)
            throw new AcordoInvalidOperationException($"Já existe um acordo ativo para o contrato {contrato.NumeroContrato}. Cancele o acordo anterior antes de criar um novo.");

        // Validar quantidade de parcelas
        if (request.QuantidadeParcelas < 1 || request.QuantidadeParcelas > 60)
            throw new AcordoInvalidOperationException("A quantidade de parcelas deve estar entre 1 e 60.");

        // Calcular dívida atualizada
        var calculoDivida = await _debtCalculationService.CalcularDividaByContratoIdAsync(request.ContratoId);
        var valorTotalDivida = calculoDivida.SaldoDevedorAtualizado;

        // Calcular desconto
        decimal valorDesconto = 0;
        if (request.ValorDesconto.HasValue && request.ValorDesconto.Value > 0)
        {
            valorDesconto = request.ValorDesconto.Value;
        }
        else if (request.PercentualDesconto.HasValue && request.PercentualDesconto.Value > 0)
        {
            if (request.PercentualDesconto.Value > 100)
                throw new AcordoInvalidOperationException("O percentual de desconto não pode ser maior que 100%.");
            valorDesconto = valorTotalDivida * (request.PercentualDesconto.Value / 100);
        }

        // Validar desconto máximo (exemplo: 50% da dívida)
        var descontoMaximo = valorTotalDivida * 0.50m;
        if (valorDesconto > descontoMaximo)
            throw new AcordoInvalidOperationException($"O desconto máximo permitido é de 50% da dívida (R$ {descontoMaximo:N2}).");

        // Calcular valor total do acordo
        var valorTotalAcordo = valorTotalDivida - valorDesconto;

        // Validar entrada
        decimal valorEntrada = request.ValorEntrada ?? 0;
        if (valorEntrada < 0)
            throw new AcordoInvalidOperationException("O valor de entrada não pode ser negativo.");

        if (valorEntrada > valorTotalAcordo)
            throw new AcordoInvalidOperationException("O valor de entrada não pode ser maior que o valor total do acordo.");

        // Calcular valor das parcelas
        var valorParcelar = valorTotalAcordo - valorEntrada;
        var valorParcela = request.QuantidadeParcelas > 0 ? valorParcelar / request.QuantidadeParcelas : 0;

        // Validar valor mínimo de parcela (exemplo: R$ 50,00)
        if (valorParcela < 50 && request.QuantidadeParcelas > 0)
            throw new AcordoInvalidOperationException($"O valor mínimo da parcela é de R$ 50,00. Reduza a quantidade de parcelas ou aumente o valor de entrada.");

        // Validar data do primeiro vencimento
        if (request.DataPrimeiroVencimento < DateTime.Now.Date)
            throw new AcordoInvalidOperationException("A data do primeiro vencimento não pode ser anterior à data atual.");

        // Gerar número do acordo
        var numeroAcordo = $"ACO-{DateTime.Now:yyyyMMddHHmmss}-{request.ContratoId}";

        // Criar acordo
        var acordo = new Acordo
        {
            ContratoId = request.ContratoId,
            NumeroAcordo = numeroAcordo,
            ValorTotalDivida = valorTotalDivida,
            ValorDesconto = valorDesconto,
            ValorTotalAcordo = valorTotalAcordo,
            ValorEntrada = valorEntrada > 0 ? valorEntrada : null,
            QuantidadeParcelas = request.QuantidadeParcelas,
            ValorParcela = valorParcela,
            DataPrimeiroVencimento = request.DataPrimeiroVencimento,
            DataAcordo = DateTime.Now,
            Status = StatusAcordo.Ativo,
            Observacoes = request.Observacoes,
            DataCadastro = DateTime.Now
        };

        var acordoCriado = await _acordoRepository.AddAsync(acordo);

        // Criar parcelas do acordo
        var parcelas = new List<ParcelaAcordo>();
        for (int i = 1; i <= request.QuantidadeParcelas; i++)
        {
            var dataVencimentoParcela = request.DataPrimeiroVencimento.AddMonths(i - 1);

            var parcelaAcordo = new ParcelaAcordo
            {
                AcordoId = acordoCriado.Id,
                NumeroParcela = i,
                Valor = valorParcela,
                DataVencimento = dataVencimentoParcela,
                Status = StatusParcelaAcordo.Pendente,
                DataCadastro = DateTime.Now
            };

            var parcelaCriada = await _parcelaAcordoRepository.AddAsync(parcelaAcordo);
            parcelas.Add(parcelaCriada);
        }

        // Atualizar status do contrato para EmAcordo
        contrato.Status = StatusContrato.EmAcordo;
        contrato.DataAtualizacao = DateTime.Now;
        await _contratoRepository.UpdateAsync(contrato);

        // Atualizar parcelas do contrato para EmAcordo
        var parcelasContrato = contrato.Parcelas.Where(p => p.Status == StatusParcela.Aberta || p.Status == StatusParcela.Vencida);
        foreach (var parcela in parcelasContrato)
        {
            parcela.Status = StatusParcela.EmAcordo;
            parcela.DataAtualizacao = DateTime.Now;
        }

        return MapToResponseDto(acordoCriado, contrato);
    }

    public async Task<AcordoResponseDto?> GetByIdAsync(int id)
    {
        var acordo = await _acordoRepository.GetByIdAsync(id);
        if (acordo == null)
            return null;

        var contrato = await _contratoRepository.GetByIdAsync(acordo.ContratoId);
        return MapToResponseDto(acordo, contrato!);
    }

    public async Task<AcordoResponseDto?> GetByNumeroAcordoAsync(string numeroAcordo)
    {
        var acordo = await _acordoRepository.GetByNumeroAcordoAsync(numeroAcordo);
        if (acordo == null)
            return null;

        var contrato = await _contratoRepository.GetByIdAsync(acordo.ContratoId);
        return MapToResponseDto(acordo, contrato!);
    }

    public async Task<IEnumerable<AcordoResponseDto>> GetByContratoIdAsync(int contratoId)
    {
        var acordos = await _acordoRepository.GetByContratoIdAsync(contratoId);
        var contrato = await _contratoRepository.GetByIdAsync(contratoId);

        return acordos.Select(a => MapToResponseDto(a, contrato!));
    }

    public async Task<IEnumerable<AcordoResponseDto>> GetAllAsync()
    {
        var acordos = await _acordoRepository.GetAllAsync();
        var result = new List<AcordoResponseDto>();

        foreach (var acordo in acordos)
        {
            var contrato = await _contratoRepository.GetByIdAsync(acordo.ContratoId);
            result.Add(MapToResponseDto(acordo, contrato!));
        }

        return result;
    }

    public async Task<bool> CancelarAcordoAsync(int id)
    {
        var acordo = await _acordoRepository.GetByIdAsync(id);
        if (acordo == null)
            throw new AcordoNotFoundException(id);

        if (acordo.Status != StatusAcordo.Ativo)
            throw new AcordoInvalidOperationException($"Apenas acordos ativos podem ser cancelados. Status atual: {acordo.Status}");

        acordo.Status = StatusAcordo.Cancelado;
        acordo.DataAtualizacao = DateTime.Now;
        await _acordoRepository.UpdateAsync(acordo);

        // Reverter status do contrato
        var contrato = await _contratoRepository.GetByIdAsync(acordo.ContratoId);
        if (contrato != null && contrato.Status == StatusContrato.EmAcordo)
        {
            contrato.Status = StatusContrato.Inadimplente;
            contrato.DataAtualizacao = DateTime.Now;
            await _contratoRepository.UpdateAsync(contrato);
        }

        return true;
    }

    private AcordoResponseDto MapToResponseDto(Acordo acordo, Contrato contrato)
    {
        var percentualDesconto = acordo.ValorTotalDivida > 0
            ? (acordo.ValorDesconto / acordo.ValorTotalDivida) * 100
            : 0;

        return new AcordoResponseDto
        {
            Id = acordo.Id,
            ContratoId = acordo.ContratoId,
            NumeroAcordo = acordo.NumeroAcordo,
            NumeroContrato = contrato.NumeroContrato,
            ClienteNome = contrato.Cliente?.Nome ?? string.Empty,
            ValorTotalDivida = acordo.ValorTotalDivida,
            ValorDesconto = acordo.ValorDesconto,
            PercentualDesconto = percentualDesconto,
            ValorTotalAcordo = acordo.ValorTotalAcordo,
            ValorEntrada = acordo.ValorEntrada,
            QuantidadeParcelas = acordo.QuantidadeParcelas,
            ValorParcela = acordo.ValorParcela,
            DataPrimeiroVencimento = acordo.DataPrimeiroVencimento,
            DataAcordo = acordo.DataAcordo,
            Status = acordo.Status,
            StatusDescricao = acordo.Status.ToString(),
            Observacoes = acordo.Observacoes
        };
    }
}
