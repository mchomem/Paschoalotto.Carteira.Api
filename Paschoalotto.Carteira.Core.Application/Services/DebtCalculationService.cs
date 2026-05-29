namespace Paschoalotto.Carteira.Core.Application.Services;

public class DebtCalculationService : IDebtCalculationService
{
    private readonly IContratoRepository _contratoRepository;
    private readonly IParcelaRepository _parcelaRepository;

    public DebtCalculationService(
        IContratoRepository contratoRepository,
        IParcelaRepository parcelaRepository)
    {
        _contratoRepository = contratoRepository;
        _parcelaRepository = parcelaRepository;
    }

    public async Task<CalculoDividaResponseDto> CalcularDividaAsync(CalculoDividaRequestDto request)
    {
        return await CalcularDividaByContratoIdAsync(request.ContratoId, request.DataReferencia);
    }

    public async Task<CalculoDividaResponseDto> CalcularDividaByContratoIdAsync(int contratoId, DateTime? dataReferencia = null)
    {
        var contrato = await _contratoRepository.GetWithParcelasAsync(contratoId);
        if (contrato == null)
            throw new ContratoNotFoundException(contratoId);

        var dataCalculo = dataReferencia ?? DateTime.Now.Date;

        // Obter parcelas em aberto
        var parcelasAbertas = contrato.Parcelas
            .Where(p => p.Status == StatusParcela.Aberta || p.Status == StatusParcela.Vencida)
            .ToList();

        decimal valorTotalJuros = 0;
        decimal valorTotalMulta = 0;
        decimal valorTotalCorrecao = 0;
        decimal saldoDevedorAtualizado = 0;

        var parcelasCalculo = new List<ParcelaCalculoDto>();

        foreach (var parcela in parcelasAbertas)
        {
            // Calcular dias de atraso
            var diasAtraso = 0;
            if (dataCalculo > parcela.DataVencimento)
            {
                diasAtraso = (dataCalculo - parcela.DataVencimento).Days;
            }

            decimal valorOriginalParcela = parcela.ValorOriginal;
            decimal valorJurosParcela = 0;
            decimal valorMultaParcela = 0;
            decimal valorCorrecaoParcela = 0;

            if (diasAtraso > 0)
            {
                // Cálculo de Juros Compostos (Fórmula de mercado)
                // M = C * (1 + i)^n
                // Onde: M = montante, C = capital inicial, i = taxa mensal, n = meses
                var meses = diasAtraso / 30.0; // Converte dias em meses
                var taxaJurosDecimal = contrato.TaxaJurosMensal / 100;
                var montanteComJuros = valorOriginalParcela * (decimal)Math.Pow((double)(1 + taxaJurosDecimal), meses);
                valorJurosParcela = montanteComJuros - valorOriginalParcela;

                // Cálculo de Multa (percentual fixo sobre o valor original)
                valorMultaParcela = valorOriginalParcela * (contrato.TaxaMulta / 100);

                // Cálculo de Correção Monetária (aplicada sobre valor original + juros)
                // Simulação simplificada: taxa mensal acumulada
                var taxaCorrecaoDecimal = contrato.TaxaCorrecaoMonetaria / 100;
                valorCorrecaoParcela = (valorOriginalParcela + valorJurosParcela) * (decimal)meses * taxaCorrecaoDecimal;
            }

            var valorAtualizadoParcela = valorOriginalParcela + valorJurosParcela + valorMultaParcela + valorCorrecaoParcela;

            valorTotalJuros += valorJurosParcela;
            valorTotalMulta += valorMultaParcela;
            valorTotalCorrecao += valorCorrecaoParcela;
            saldoDevedorAtualizado += valorAtualizadoParcela;

            parcelasCalculo.Add(new ParcelaCalculoDto
            {
                ParcelaId = parcela.Id,
                NumeroParcela = parcela.NumeroParcela,
                ValorOriginal = valorOriginalParcela,
                ValorAtualizado = valorAtualizadoParcela,
                DataVencimento = parcela.DataVencimento,
                DiasAtraso = diasAtraso,
                ValorJuros = valorJurosParcela,
                ValorMulta = valorMultaParcela,
                ValorCorrecao = valorCorrecaoParcela
            });

            // Atualizar a parcela com valores calculados
            parcela.ValorAtualizado = valorAtualizadoParcela;
            parcela.DiasAtraso = diasAtraso > 0 ? diasAtraso : null;
            if (diasAtraso > 0 && parcela.Status == StatusParcela.Aberta)
            {
                parcela.Status = StatusParcela.Vencida;
            }
            await _parcelaRepository.UpdateAsync(parcela);
        }

        var quantidadeParcelasVencidas = parcelasAbertas.Count(p => p.Status == StatusParcela.Vencida);

        // Atualizar saldo devedor do contrato
        contrato.SaldoDevedor = saldoDevedorAtualizado;
        if (quantidadeParcelasVencidas > 0 && contrato.Status == StatusContrato.Ativo)
        {
            contrato.Status = StatusContrato.Inadimplente;
        }
        contrato.DataAtualizacao = DateTime.Now;
        await _contratoRepository.UpdateAsync(contrato);

        var observacoes = $"Cálculo realizado em {dataCalculo:dd/MM/yyyy}. ";
        if (quantidadeParcelasVencidas > 0)
        {
            observacoes += $"Contrato possui {quantidadeParcelasVencidas} parcela(s) vencida(s). ";
        }
        observacoes += "Valores calculados: juros compostos, multa contratual e correção monetária.";

        return new CalculoDividaResponseDto
        {
            ContratoId = contrato.Id,
            NumeroContrato = contrato.NumeroContrato,
            DataCalculo = dataCalculo,
            ValorOriginalContrato = contrato.ValorOriginal,
            SaldoDevedorAtualizado = saldoDevedorAtualizado,
            ValorJuros = valorTotalJuros,
            ValorMulta = valorTotalMulta,
            ValorCorrecaoMonetaria = valorTotalCorrecao,
            QuantidadeParcelasAbertas = parcelasAbertas.Count,
            QuantidadeParcelasVencidas = quantidadeParcelasVencidas,
            ParcelasEmAberto = parcelasCalculo,
            Observacoes = observacoes
        };
    }
}
