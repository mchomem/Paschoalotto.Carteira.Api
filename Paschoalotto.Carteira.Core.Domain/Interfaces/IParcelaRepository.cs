using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IParcelaRepository : IRepositoryBase<Parcela>
{
    Task<IEnumerable<Parcela>> GetByContratoIdAsync(int contratoId);
    Task<IEnumerable<Parcela>> GetParcelasAbertasByContratoIdAsync(int contratoId);
    Task<IEnumerable<Parcela>> GetParcelasVencidasByContratoIdAsync(int contratoId);
    Task<IEnumerable<Parcela>> GetByStatusAsync(Enums.StatusParcela status);
    Task<int> CountParcelasAbertasByContratoIdAsync(int contratoId);
}
