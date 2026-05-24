using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class ParcelaRepository : RepositoryBase<Parcela>, IParcelaRepository
{
    public ParcelaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Parcela>> GetByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .Where(p => p.ContratoId == contratoId)
            .OrderBy(p => p.NumeroParcela)
            .ToListAsync();
    }

    public async Task<IEnumerable<Parcela>> GetParcelasAbertasByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .Where(p => p.ContratoId == contratoId && p.Status == StatusParcela.Aberta)
            .OrderBy(p => p.NumeroParcela)
            .ToListAsync();
    }

    public async Task<IEnumerable<Parcela>> GetParcelasVencidasByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .Where(p => p.ContratoId == contratoId && p.Status == StatusParcela.Vencida)
            .OrderBy(p => p.NumeroParcela)
            .ToListAsync();
    }

    public async Task<IEnumerable<Parcela>> GetByStatusAsync(StatusParcela status)
    {
        return await _dbSet
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<int> CountParcelasAbertasByContratoIdAsync(int contratoId)
    {
        return await _dbSet
            .CountAsync(p => p.ContratoId == contratoId && p.Status == StatusParcela.Aberta);
    }
}
