using Paschoalotto.Carteira.Core.Application.DTOs.ParcelaAcordo;
using Paschoalotto.Carteira.Core.Application.Interfaces;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;

namespace Paschoalotto.Carteira.Core.Application.Services;

public class ParcelaAcordoService : IParcelaAcordoService
{
    private readonly IParcelaAcordoRepository _parcelaAcordoRepository;

    public ParcelaAcordoService(IParcelaAcordoRepository parcelaAcordoRepository)
    {
        _parcelaAcordoRepository = parcelaAcordoRepository;
    }

    public async Task<ParcelaAcordoResponseDto?> GetByIdAsync(int id)
    {
        var parcela = await _parcelaAcordoRepository.GetWithBoletoAsync(id);
        if (parcela == null)
            return null;

        return MapToResponseDto(parcela);
    }

    public async Task<IEnumerable<ParcelaAcordoResponseDto>> GetByAcordoIdAsync(int acordoId)
    {
        var parcelas = await _parcelaAcordoRepository.GetByAcordoIdAsync(acordoId);
        return parcelas.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ParcelaAcordoResponseDto>> GetParcelasVencidasAsync()
    {
        var parcelas = await _parcelaAcordoRepository.GetParcelasVencidasAsync();
        return parcelas.Select(MapToResponseDto);
    }

    private ParcelaAcordoResponseDto MapToResponseDto(Core.Domain.Entities.ParcelaAcordo parcela)
    {
        return new ParcelaAcordoResponseDto
        {
            Id = parcela.Id,
            AcordoId = parcela.AcordoId,
            NumeroParcela = parcela.NumeroParcela,
            Valor = parcela.Valor,
            DataVencimento = parcela.DataVencimento,
            DataPagamento = parcela.DataPagamento,
            ValorPago = parcela.ValorPago,
            Status = parcela.Status,
            StatusDescricao = parcela.Status.ToString(),
            TemBoleto = parcela.Boleto != null,
            BoletoId = parcela.Boleto?.Id
        };
    }
}
