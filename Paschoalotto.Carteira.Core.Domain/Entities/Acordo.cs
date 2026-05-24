using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class Acordo
{
    public int Id { get; set; }
    public int ContratoId { get; set; }
    public string NumeroAcordo { get; set; } = string.Empty;
    public decimal ValorTotalDivida { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorTotalAcordo { get; set; }
    public decimal? ValorEntrada { get; set; }
    public int QuantidadeParcelas { get; set; }
    public decimal ValorParcela { get; set; }
    public DateTime DataPrimeiroVencimento { get; set; }
    public DateTime DataAcordo { get; set; }
    public StatusAcordo Status { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos
    public Contrato Contrato { get; set; } = null!;
    public ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
}
