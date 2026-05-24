using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Mappings;

public class BoletoMapping : IEntityTypeConfiguration<Boleto>
{
    public void Configure(EntityTypeBuilder<Boleto> builder)
    {
        builder.ToTable("Boletos");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.NossoNumero)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(b => b.NossoNumero)
            .IsUnique()
            .HasDatabaseName("IX_Boletos_NossoNumero");

        builder.Property(b => b.LinhaDigitavel)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.CodigoBarras)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Valor)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(b => b.DataVencimento)
            .IsRequired();

        builder.Property(b => b.DataPagamento);

        builder.Property(b => b.ValorPago)
            .HasPrecision(18, 2);

        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(b => b.DocumentoPdfBase64)
            .HasColumnType("text");

        builder.Property(b => b.DataEmissao)
            .IsRequired();

        builder.Property(b => b.DataAtualizacao);

        // Relacionamentos
        builder.HasOne(b => b.Acordo)
            .WithMany(a => a.Boletos)
            .HasForeignKey(b => b.AcordoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
