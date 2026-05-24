using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Core.Domain.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> GetByDocumentoAsync(string documento);
    Task<bool> ExistsByDocumentoAsync(string documento);
    Task<IEnumerable<Cliente>> GetByTipoPessoaAsync(Enums.TipoPessoa tipoPessoa);
    Task<IEnumerable<Cliente>> SearchAsync(string searchTerm);
}
