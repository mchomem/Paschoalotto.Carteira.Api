using Microsoft.EntityFrameworkCore;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Parcela> Parcelas { get; set; }
    public DbSet<Acordo> Acordos { get; set; }
    public DbSet<Boleto> Boletos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas as configurações de mapeamento automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
