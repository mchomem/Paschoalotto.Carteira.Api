using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;
using Paschoalotto.Carteira.Core.Domain.Enums;
using Paschoalotto.Carteira.Core.Domain.Interfaces;
using Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Repositories;

public class ContratoRepository : RepositoryBase<Contrato>, IContratoRepository
{
    public ContratoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Contrato?> GetByNumeroContratoAsync(string numeroContrato)
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .FirstOrDefaultAsync(c => c.NumeroContrato == numeroContrato);
    }

    public async Task<IEnumerable<Contrato>> GetByClienteIdAsync(int clienteId)
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .Where(c => c.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<Contrato?> GetWithParcelasAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .Include(c => c.Parcelas)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contrato?> GetWithParcelasAndAcordosAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .Include(c => c.Parcelas)
            .Include(c => c.Acordos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Contrato>> GetByStatusAsync(StatusContrato status)
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .Where(c => c.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Contrato>> GetContratosInadimplentesAsync()
    {
        return await _dbSet
            .Include(c => c.Cliente)
            .Where(c => c.Status == StatusContrato.Inadimplente)
            .ToListAsync();
    }
}
