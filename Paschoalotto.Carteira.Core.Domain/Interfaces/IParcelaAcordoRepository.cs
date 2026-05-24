using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IParcelaAcordoRepository : IRepositoryBase<ParcelaAcordo>
{
    Task<IEnumerable<ParcelaAcordo>> GetByAcordoIdAsync(int acordoId);
    Task<ParcelaAcordo?> GetByAcordoIdAndNumeroParcelaAsync(int acordoId, int numeroParcela);
    Task<IEnumerable<ParcelaAcordo>> GetByStatusAsync(StatusParcelaAcordo status);
    Task<IEnumerable<ParcelaAcordo>> GetParcelasVencidasAsync();
    Task<ParcelaAcordo?> GetWithBoletoAsync(int id);
}
