using Paschoalotto.Carteira.Core.Application.DTOs.Calculo;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IDebtCalculationService
{
    Task<CalculoDividaResponseDto> CalcularDividaAsync(CalculoDividaRequestDto request);
    Task<CalculoDividaResponseDto> CalcularDividaByContratoIdAsync(int contratoId, DateTime? dataReferencia = null);
}
