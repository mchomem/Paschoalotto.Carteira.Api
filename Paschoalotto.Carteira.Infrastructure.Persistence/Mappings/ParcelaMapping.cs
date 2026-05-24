using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Mappings;

public class ParcelaMapping : IEntityTypeConfiguration<Parcela>
{
    public void Configure(EntityTypeBuilder<Parcela> builder)
    {
        builder.ToTable("Parcelas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.NumeroParcela)
            .IsRequired();

        builder.Property(p => p.ValorOriginal)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.ValorAtualizado)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.DataVencimento)
            .IsRequired();

        builder.Property(p => p.DataPagamento);

        builder.Property(p => p.ValorPago)
            .HasPrecision(18, 2);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.DiasAtraso);

        builder.Property(p => p.DataCadastro)
            .IsRequired();

        builder.Property(p => p.DataAtualizacao);

        // Índice composto para contrato + número da parcela
        builder.HasIndex(p => new { p.ContratoId, p.NumeroParcela })
            .HasDatabaseName("IX_Parcelas_ContratoId_NumeroParcela");

        // Relacionamentos
        builder.HasOne(p => p.Contrato)
            .WithMany(c => c.Parcelas)
            .HasForeignKey(p => p.ContratoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
