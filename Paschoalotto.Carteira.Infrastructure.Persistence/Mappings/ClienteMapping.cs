using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paschoalotto.Carteira.Core.Domain.Entities;

namespace Paschoalotto.Carteira.Infrastructure.Persistence.Mappings;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.TipoPessoa)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Documento)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Documento)
            .IsUnique()
            .HasDatabaseName("IX_Clientes_Documento");

        builder.Property(c => c.Email)
            .HasMaxLength(200);

        builder.Property(c => c.Telefone)
            .HasMaxLength(20);

        builder.Property(c => c.Endereco)
            .HasMaxLength(500);

        builder.Property(c => c.Cidade)
            .HasMaxLength(100);

        builder.Property(c => c.Estado)
            .HasMaxLength(2);

        builder.Property(c => c.Cep)
            .HasMaxLength(10);

        builder.Property(c => c.DataCadastro)
            .IsRequired();

        builder.Property(c => c.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relacionamentos
        builder.HasMany(c => c.Contratos)
            .WithOne(co => co.Cliente)
            .HasForeignKey(co => co.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
