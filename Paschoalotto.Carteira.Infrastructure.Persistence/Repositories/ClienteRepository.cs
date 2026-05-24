using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> GetByDocumentoAsync(string documento)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Documento == documento);
    }

    public async Task<bool> ExistsByDocumentoAsync(string documento)
    {
        return await _dbSet.AnyAsync(c => c.Documento == documento);
    }

    public async Task<IEnumerable<Cliente>> GetByTipoPessoaAsync(TipoPessoa tipoPessoa)
    {
        return await _dbSet.Where(c => c.TipoPessoa == tipoPessoa).ToListAsync();
    }

    public async Task<IEnumerable<Cliente>> SearchAsync(string searchTerm)
    {
        return await _dbSet
            .Where(c => c.Nome.Contains(searchTerm) || c.Documento.Contains(searchTerm))
            .ToListAsync();
    }
}
