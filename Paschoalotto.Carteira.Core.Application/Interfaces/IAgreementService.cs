using Paschoalotto.Carteira.Core.Application.DTOs.Acordo;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IAgreementService
{
    Task<AcordoResponseDto> CreateAcordoAsync(AcordoRequestDto request);
    Task<AcordoResponseDto?> GetByIdAsync(int id);
    Task<AcordoResponseDto?> GetByNumeroAcordoAsync(string numeroAcordo);
    Task<IEnumerable<AcordoResponseDto>> GetByContratoIdAsync(int contratoId);
    Task<IEnumerable<AcordoResponseDto>> GetAllAsync();
    Task<bool> CancelarAcordoAsync(int id);
}
