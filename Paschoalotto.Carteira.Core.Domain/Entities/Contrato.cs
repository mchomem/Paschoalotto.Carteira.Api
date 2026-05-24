using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class Contrato
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string NumeroContrato { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public decimal SaldoDevedor { get; set; }
    public decimal TaxaJurosMensal { get; set; } // Percentual mensal
    public decimal TaxaMulta { get; set; } // Percentual de multa
    public decimal TaxaCorrecaoMonetaria { get; set; } // Percentual de correção
    public DateTime DataContrato { get; set; }
    public DateTime DataVencimento { get; set; }
    public StatusContrato Status { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos
    public Cliente Cliente { get; set; } = null!;
    public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    public ICollection<Acordo> Acordos { get; set; } = new List<Acordo>();
}
