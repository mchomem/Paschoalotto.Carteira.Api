using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class BoletoRepository : RepositoryBase<Boleto>, IBoletoRepository
{
    public BoletoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Boleto>> GetByAcordoIdAsync(int acordoId)
    {
        return await _dbSet
            .Where(b => b.AcordoId == acordoId)
            .OrderBy(b => b.DataVencimento)
            .ToListAsync();
    }

    public async Task<Boleto?> GetByNossoNumeroAsync(string nossoNumero)
    {
        return await _dbSet
            .Include(b => b.Acordo)
                .ThenInclude(a => a.Contrato)
                    .ThenInclude(c => c.Cliente)
            .FirstOrDefaultAsync(b => b.NossoNumero == nossoNumero);
    }

    public async Task<IEnumerable<Boleto>> GetByStatusAsync(StatusBoleto status)
    {
        return await _dbSet
            .Where(b => b.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Boleto>> GetBoletosVencidosAsync()
    {
        var hoje = DateTime.Now.Date;
        return await _dbSet
            .Where(b => b.Status == StatusBoleto.Pendente && b.DataVencimento < hoje)
            .ToListAsync();
    }

    public async Task<IEnumerable<Boleto>> GetBoletosPendentesAsync()
    {
        return await _dbSet
            .Where(b => b.Status == StatusBoleto.Pendente)
            .ToListAsync();
    }
}
