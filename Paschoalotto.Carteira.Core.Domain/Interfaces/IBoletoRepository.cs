using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IBoletoRepository : IRepositoryBase<Boleto>
{
    Task<IEnumerable<Boleto>> GetByAcordoIdAsync(int acordoId);
    Task<Boleto?> GetByNossoNumeroAsync(string nossoNumero);
    Task<IEnumerable<Boleto>> GetByStatusAsync(Enums.StatusBoleto status);
    Task<IEnumerable<Boleto>> GetBoletosVencidosAsync();
    Task<IEnumerable<Boleto>> GetBoletosPendentesAsync();
}
