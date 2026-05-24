using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class Parcela
{
    public int Id { get; set; }
    public int ContratoId { get; set; }
    public int NumeroParcela { get; set; }
    public decimal ValorOriginal { get; set; }
    public decimal ValorAtualizado { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusParcela Status { get; set; }
    public int? DiasAtraso { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos
    public Contrato Contrato { get; set; } = null!;
}
