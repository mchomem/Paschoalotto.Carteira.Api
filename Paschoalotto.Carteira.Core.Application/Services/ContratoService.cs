using Mapster;
using Paschoalotto.Carteira.Core.Application.DTOs.Contrato;
using Paschoalotto.Carteira.Core.Application.Interfaces;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Cliente;
using Paschoalotto.Carteira.Core.Domain.Exceptions.Contrato;
using Paschoalotto.Carteira.Core.Domain.Interfaces;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class ContratoService : IContratoService
{
    private readonly IContratoRepository _contratoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IParcelaRepository _parcelaRepository;

    public ContratoService(
        IContratoRepository contratoRepository,
        IClienteRepository clienteRepository,
        IParcelaRepository parcelaRepository)
    {
        _contratoRepository = contratoRepository;
        _clienteRepository = clienteRepository;
        _parcelaRepository = parcelaRepository;
    }

    public async Task<ContratoResponseDto> CreateAsync(ContratoRequestDto request)
    {
        // Validar se cliente existe
        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
        if (cliente == null)
            throw new ClienteNotFoundException(request.ClienteId);

        var contrato = request.Adapt<Contrato>();
        contrato.SaldoDevedor = request.ValorOriginal;
        contrato.Status = StatusContrato.Ativo;
        contrato.DataCadastro = DateTime.Now;

        var contratoCriado = await _contratoRepository.AddAsync(contrato);
        return MapToResponseDto(contratoCriado, cliente);
    }

    public async Task<ContratoResponseDto> UpdateAsync(int id, ContratoRequestDto request)
    {
        var contrato = await _contratoRepository.GetByIdAsync(id);
        if (contrato == null)
            throw new ContratoNotFoundException(id);

        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
        if (cliente == null)
            throw new ClienteNotFoundException(request.ClienteId);

        contrato.ClienteId = request.ClienteId;
        contrato.NumeroContrato = request.NumeroContrato;
        contrato.ValorOriginal = request.ValorOriginal;
        contrato.TaxaJurosMensal = request.TaxaJurosMensal;
        contrato.TaxaMulta = request.TaxaMulta;
        contrato.TaxaCorrecaoMonetaria = request.TaxaCorrecaoMonetaria;
        contrato.DataContrato = request.DataContrato;
        contrato.DataVencimento = request.DataVencimento;
        contrato.Observacoes = request.Observacoes;
        contrato.DataAtualizacao = DateTime.Now;

        var contratoAtualizado = await _contratoRepository.UpdateAsync(contrato);
        return MapToResponseDto(contratoAtualizado, cliente);
    }

    public async Task<ContratoResponseDto?> GetByIdAsync(int id)
    {
        var contrato = await _contratoRepository.GetByIdAsync(id);
        if (contrato == null)
            return null;

        var cliente = await _clienteRepository.GetByIdAsync(contrato.ClienteId);
        return MapToResponseDto(contrato, cliente!);
    }

    public async Task<ContratoResponseDto?> GetByNumeroContratoAsync(string numeroContrato)
    {
        var contrato = await _contratoRepository.GetByNumeroContratoAsync(numeroContrato);
        if (contrato == null)
            return null;

        var cliente = await _clienteRepository.GetByIdAsync(contrato.ClienteId);
        return MapToResponseDto(contrato, cliente!);
    }

    public async Task<IEnumerable<ContratoResponseDto>> GetByClienteIdAsync(int clienteId)
    {
        var contratos = await _contratoRepository.GetByClienteIdAsync(clienteId);
        var cliente = await _clienteRepository.GetByIdAsync(clienteId);

        return contratos.Select(c => MapToResponseDto(c, cliente!));
    }

    public async Task<IEnumerable<ContratoResponseDto>> GetAllAsync()
    {
        var contratos = await _contratoRepository.GetAllAsync();
        var result = new List<ContratoResponseDto>();

        foreach (var contrato in contratos)
        {
            var cliente = await _clienteRepository.GetByIdAsync(contrato.ClienteId);
            result.Add(MapToResponseDto(contrato, cliente!));
        }

        return result;
    }

    public async Task<IEnumerable<ParcelaResponseDto>> GetParcelasByContratoIdAsync(int contratoId)
    {
        var parcelas = await _parcelaRepository.GetByContratoIdAsync(contratoId);
        return parcelas.Select(p => new ParcelaResponseDto
        {
            Id = p.Id,
            NumeroParcela = p.NumeroParcela,
            ValorOriginal = p.ValorOriginal,
            ValorAtualizado = p.ValorAtualizado,
            DataVencimento = p.DataVencimento,
            DataPagamento = p.DataPagamento,
            ValorPago = p.ValorPago,
            Status = p.Status.ToString(),
            DiasAtraso = p.DiasAtraso
        });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existe = await _contratoRepository.ExistsAsync(id);
        if (!existe)
            throw new ContratoNotFoundException(id);

        return await _contratoRepository.DeleteAsync(id);
    }

    private ContratoResponseDto MapToResponseDto(Contrato contrato, Cliente cliente)
    {
        return new ContratoResponseDto
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
}
