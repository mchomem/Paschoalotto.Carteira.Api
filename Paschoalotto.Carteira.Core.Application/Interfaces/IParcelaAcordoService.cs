using Paschoalotto.Carteira.Core.Application.DTOs.ParcelaAcordo;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IParcelaAcordoService
{
    Task<ParcelaAcordoResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<ParcelaAcordoResponseDto>> GetByAcordoIdAsync(int acordoId);
    Task<IEnumerable<ParcelaAcordoResponseDto>> GetParcelasVencidasAsync();
}
