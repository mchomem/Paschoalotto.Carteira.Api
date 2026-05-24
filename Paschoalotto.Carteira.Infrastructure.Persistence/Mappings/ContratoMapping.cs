using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Mappings;

public class ContratoMapping : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("Contratos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.NumeroContrato)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.NumeroContrato)
            .IsUnique()
            .HasDatabaseName("IX_Contratos_NumeroContrato");

        builder.Property(c => c.ValorOriginal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.SaldoDevedor)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.TaxaJurosMensal)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(c => c.TaxaMulta)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(c => c.TaxaCorrecaoMonetaria)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(c => c.DataContrato)
            .IsRequired();

        builder.Property(c => c.DataVencimento)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.Observacoes)
            .HasMaxLength(1000);

        builder.Property(c => c.DataCadastro)
            .IsRequired();

        builder.Property(c => c.DataAtualizacao);

        // Relacionamentos
        builder.HasOne(c => c.Cliente)
            .WithMany(cl => cl.Contratos)
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Parcelas)
            .WithOne(p => p.Contrato)
            .HasForeignKey(p => p.ContratoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Acordos)
            .WithOne(a => a.Contrato)
            .HasForeignKey(a => a.ContratoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
