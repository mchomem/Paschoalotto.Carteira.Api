using Mapster;
using Paschoalotto.Carteira.Core.Application.DTOs.Cliente;
using Paschoalotto.Carteira.Core.Application.Interfaces;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Cliente;
using Paschoalotto.Carteira.Core.Domain.Interfaces;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IContratoRepository _contratoRepository;
    private readonly IParcelaRepository _parcelaRepository;
    private readonly IAcordoRepository _acordoRepository;
    private readonly IBoletoRepository _boletoRepository;

    public ClienteService(
        IClienteRepository clienteRepository,
        IContratoRepository contratoRepository,
        IParcelaRepository parcelaRepository,
        IAcordoRepository acordoRepository,
        IBoletoRepository boletoRepository)
    {
        _clienteRepository = clienteRepository;
        _contratoRepository = contratoRepository;
        _parcelaRepository = parcelaRepository;
        _acordoRepository = acordoRepository;
        _boletoRepository = boletoRepository;
    }

    public async Task<ClienteResponseDto> CreateAsync(ClienteRequestDto request)
    {
        // Validar se documento já existe
        if (await _clienteRepository.ExistsByDocumentoAsync(request.Documento))
            throw new ClienteAlreadyExistsException(request.Documento);

        var cliente = request.Adapt<Cliente>();
        cliente.DataCadastro = DateTime.Now;
        cliente.Ativo = true;

        var clienteCriado = await _clienteRepository.AddAsync(cliente);
        return MapToResponseDto(clienteCriado);
    }

    public async Task<ClienteResponseDto> UpdateAsync(int id, ClienteRequestDto request)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
            throw new ClienteNotFoundException(id);

        // Validar se o novo documento já pertence a outro cliente
        var clienteComMesmoDocumento = await _clienteRepository.GetByDocumentoAsync(request.Documento);
        if (clienteComMesmoDocumento != null && clienteComMesmoDocumento.Id != id)
            throw new ClienteAlreadyExistsException(request.Documento);

        cliente.TipoPessoa = request.TipoPessoa;
        cliente.Nome = request.Nome;
        cliente.Documento = request.Documento;
        cliente.Email = request.Email;
        cliente.Telefone = request.Telefone;
        cliente.Endereco = request.Endereco;
        cliente.Cidade = request.Cidade;
        cliente.Estado = request.Estado;
        cliente.Cep = request.Cep;

        var clienteAtualizado = await _clienteRepository.UpdateAsync(cliente);
        return MapToResponseDto(clienteAtualizado);
    }

    public async Task<ClienteResponseDto?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        return cliente != null ? MapToResponseDto(cliente) : null;
    }

    public async Task<ClienteResponseDto?> GetByDocumentoAsync(string documento)
    {
        var cliente = await _clienteRepository.GetByDocumentoAsync(documento);
        return cliente != null ? MapToResponseDto(cliente) : null;
    }

    public async Task<ClienteDashboardDto?> GetDashboardByDocumentoAsync(string documento)
    {
        var cliente = await _clienteRepository.GetByDocumentoAsync(documento);
        if (cliente == null)
            return null;

        var contratos = await _contratoRepository.GetByClienteIdAsync(cliente.Id);
        var contratosDashboard = new List<ContratoDashboardDto>();

        foreach (var contrato in contratos)
        {
            // Buscar parcelas em aberto do contrato
            var parcelasEmAberto = await _parcelaRepository.GetParcelasAbertasByContratoIdAsync(contrato.Id);

            // Buscar acordo ativo
            var acordoAtivo = await _acordoRepository.GetAcordoAtivoByContratoIdAsync(contrato.Id);

            // Buscar boletos pendentes do acordo
            var boletosPendentes = new List<Boleto>();
            if (acordoAtivo != null)
            {
                var todosBoletosAcordo = await _boletoRepository.GetByAcordoIdAsync(acordoAtivo.Id);
                boletosPendentes = todosBoletosAcordo.Where(b => b.Status == StatusBoleto.Pendente).ToList();
            }

            contratosDashboard.Add(new ContratoDashboardDto
            {
                Contrato = MapContratoToResponseDto(contrato, cliente),
                ParcelasEmAberto = parcelasEmAberto.Select(MapParcelaToResponseDto).ToList(),
                AcordoAtivo = acordoAtivo != null ? MapAcordoToResponseDto(acordoAtivo, contrato, cliente) : null,
                BoletosPendentes = boletosPendentes.Select(MapBoletoToResponseDto).ToList()
            });
        }

        return new ClienteDashboardDto
        {
            Cliente = MapToResponseDto(cliente),
            Contratos = contratosDashboard
        };
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetAllAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return clientes.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClienteResponseDto>> SearchAsync(string searchTerm)
    {
        var clientes = await _clienteRepository.SearchAsync(searchTerm);
        return clientes.Select(MapToResponseDto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existe = await _clienteRepository.ExistsAsync(id);
        if (!existe)
            throw new ClienteNotFoundException(id);

        return await _clienteRepository.DeleteAsync(id);
    }

    private ClienteResponseDto MapToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto
        {
            Id = cliente.Id,
            TipoPessoa = cliente.TipoPessoa,
            TipoPessoaDescricao = cliente.TipoPessoa.ToString(),
            Nome = cliente.Nome,
            Documento = cliente.Documento,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Endereco = cliente.Endereco,
            Cidade = cliente.Cidade,
            Estado = cliente.Estado,
            Cep = cliente.Cep,
            DataCadastro = cliente.DataCadastro,
            Ativo = cliente.Ativo
        };
    }

    private Core.Application.DTOs.Contrato.ContratoResponseDto MapContratoToResponseDto(Contrato contrato, Cliente cliente)
    {
        return new Core.Application.DTOs.Contrato.ContratoResponseDto
        {
            Id = contrato.Id,
            ClienteId = contrato.ClienteId,
            ClienteNome = cliente.Nome,
            NumeroContrato = contrato.NumeroContrato,
            ValorOriginal = contrato.ValorOriginal,
            SaldoDevedor = contrato.SaldoDevedor,
            TaxaJurosMensal = contrato.TaxaJurosMensal,
            TaxaMulta = contrato.TaxaMulta,
            TaxaCorrecaoMonetaria = contrato.TaxaCorrecaoMonetaria,
            DataContrato = contrato.DataContrato,
            DataVencimento = contrato.DataVencimento,
            Status = contrato.Status,
            StatusDescricao = contrato.Status.ToString(),
            Observacoes = contrato.Observacoes,
            DataCadastro = contrato.DataCadastro
        };
    }

    private Core.Application.DTOs.Contrato.ParcelaResponseDto MapParcelaToResponseDto(Parcela parcela)
    {
        return new Core.Application.DTOs.Contrato.ParcelaResponseDto
        {
            Id = parcela.Id,
            NumeroParcela = parcela.NumeroParcela,
            ValorOriginal = parcela.ValorOriginal,
            ValorAtualizado = parcela.ValorAtualizado,
            DataVencimento = parcela.DataVencimento,
            DataPagamento = parcela.DataPagamento,
            ValorPago = parcela.ValorPago,
            Status = parcela.Status.ToString(),
            DiasAtraso = parcela.DiasAtraso
        };
    }

    private Core.Application.DTOs.Acordo.AcordoResponseDto MapAcordoToResponseDto(Acordo acordo, Contrato contrato, Cliente cliente)
    {
        var percentualDesconto = acordo.ValorTotalDivida > 0 ? (acordo.ValorDesconto / acordo.ValorTotalDivida) * 100 : 0;

        return new Core.Application.DTOs.Acordo.AcordoResponseDto
        {
            Id = acordo.Id,
            ContratoId = acordo.ContratoId,
            NumeroAcordo = acordo.NumeroAcordo,
            NumeroContrato = contrato.NumeroContrato,
            ClienteNome = cliente.Nome,
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

    private Core.Application.DTOs.Boleto.BoletoResponseDto MapBoletoToResponseDto(Boleto boleto)
    {
        return new Core.Application.DTOs.Boleto.BoletoResponseDto
        {
            Id = boleto.Id,
            AcordoId = boleto.AcordoId,
            ParcelaAcordoId = boleto.ParcelaAcordoId,
            NumeroParcela = null, // Seria necessário carregar da parcela se houver
            NossoNumero = boleto.NossoNumero,
            LinhaDigitavel = boleto.LinhaDigitavel,
            CodigoBarras = boleto.CodigoBarras,
            Valor = boleto.Valor,
            DataVencimento = boleto.DataVencimento,
            DataPagamento = boleto.DataPagamento,
            ValorPago = boleto.ValorPago,
            Status = boleto.Status,
            StatusDescricao = boleto.Status.ToString(),
            DataEmissao = boleto.DataEmissao
        };
    }
}

