using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class AcordoRepository : RepositoryBase<Acordo>, IAcordoRepository
{
    public AcordoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Acordo?> GetByNumeroAcordoAsync(string numeroAcordo)
    {
        return await _dbSet
            .Include(a => a.Contrato)
                .ThenInclude(c => c.Cliente)
            .FirstOrDefaultAsync(a => a.NumeroAcordo == numeroAcordo);
    }

    public async Task<IEnumerable<Acordo>> GetByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .Include(a => a.Contrato)
                .ThenInclude(c => c.Cliente)
            .Where(a => a.ContratoId == contratoId)
            .OrderByDescending(a => a.DataAcordo)
            .ToListAsync();
    }

    public async Task<Acordo?> GetWithBoletosAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Contrato)
                .ThenInclude(c => c.Cliente)
            .Include(a => a.Boletos)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Acordo>> GetByStatusAsync(StatusAcordo status)
    {
        return await _dbSet
            .Include(a => a.Contrato)
                .ThenInclude(c => c.Cliente)
            .Where(a => a.Status == status)
            .ToListAsync();
    }

    public async Task<Acordo?> GetAcordoAtivoByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(a => a.ContratoId == contratoId && a.Status == StatusAcordo.Ativo);
    }
}
