using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class ParcelaAcordoRepository : RepositoryBase<ParcelaAcordo>, IParcelaAcordoRepository
{
    public ParcelaAcordoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ParcelaAcordo>> GetByAcordoIdAsync(int acordoId)
    {
        return await _dbSet
            .Include(p => p.Boleto)
            .Where(p => p.AcordoId == acordoId)
            .OrderBy(p => p.NumeroParcela)
            .ToListAsync();
    }

    public async Task<ParcelaAcordo?> GetByAcordoIdAndNumeroParcelaAsync(int acordoId, int numeroParcela)
    {
        return await _dbSet
            .Include(p => p.Boleto)
            .FirstOrDefaultAsync(p => p.AcordoId == acordoId && p.NumeroParcela == numeroParcela);
    }

    public async Task<IEnumerable<ParcelaAcordo>> GetByStatusAsync(StatusParcelaAcordo status)
    {
        return await _dbSet
            .Include(p => p.Acordo)
                .ThenInclude(a => a.Contrato)
                    .ThenInclude(c => c.Cliente)
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<ParcelaAcordo>> GetParcelasVencidasAsync()
    {
        var hoje = DateTime.Now.Date;
        return await _dbSet
            .Include(p => p.Acordo)
                .ThenInclude(a => a.Contrato)
                    .ThenInclude(c => c.Cliente)
            .Where(p => p.Status == StatusParcelaAcordo.Pendente && p.DataVencimento < hoje)
            .ToListAsync();
    }

    public async Task<ParcelaAcordo?> GetWithBoletoAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Acordo)
            .Include(p => p.Boleto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
