using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IContratoRepository : IRepositoryBase<Contrato>
{
    Task<Contrato?> GetByNumeroContratoAsync(string numeroContrato);
    Task<IEnumerable<Contrato>> GetByClienteIdAsync(int clienteId);
    Task<Contrato?> GetWithParcelasAsync(int id);
    Task<Contrato?> GetWithParcelasAndAcordosAsync(int id);
    Task<IEnumerable<Contrato>> GetByStatusAsync(Enums.StatusContrato status);
    Task<IEnumerable<Contrato>> GetContratosInadimplentesAsync();
}
