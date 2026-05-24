using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty; // CPF ou CNPJ
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }

    // Relacionamentos
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
