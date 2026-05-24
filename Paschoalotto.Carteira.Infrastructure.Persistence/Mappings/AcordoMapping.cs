using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Mappings;

public class AcordoMapping : IEntityTypeConfiguration<Acordo>
{
    public void Configure(EntityTypeBuilder<Acordo> builder)
    {
        builder.ToTable("Acordos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.NumeroAcordo)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => a.NumeroAcordo)
            .IsUnique()
            .HasDatabaseName("IX_Acordos_NumeroAcordo");

        builder.Property(a => a.ValorTotalDivida)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.ValorDesconto)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.ValorTotalAcordo)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.ValorEntrada)
            .HasPrecision(18, 2);

        builder.Property(a => a.QuantidadeParcelas)
            .IsRequired();

        builder.Property(a => a.ValorParcela)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.DataPrimeiroVencimento)
            .IsRequired();

        builder.Property(a => a.DataAcordo)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.Observacoes)
            .HasMaxLength(1000);

        builder.Property(a => a.DataCadastro)
            .IsRequired();

        builder.Property(a => a.DataAtualizacao);

        // Relacionamentos
        builder.HasOne(a => a.Contrato)
            .WithMany(c => c.Acordos)
            .HasForeignKey(a => a.ContratoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Boletos)
            .WithOne(b => b.Acordo)
            .HasForeignKey(b => b.AcordoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
