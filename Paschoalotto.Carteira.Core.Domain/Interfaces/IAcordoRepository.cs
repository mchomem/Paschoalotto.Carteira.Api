using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IAcordoRepository : IRepositoryBase<Acordo>
{
    Task<Acordo?> GetByNumeroAcordoAsync(string numeroAcordo);
    Task<IEnumerable<Acordo>> GetByContratoIdAsync(int contratoId);
    Task<Acordo?> GetWithBoletosAsync(int id);
    Task<IEnumerable<Acordo>> GetByStatusAsync(Enums.StatusAcordo status);
    Task<Acordo?> GetAcordoAtivoByContratoIdAsync(int contratoId);
}
