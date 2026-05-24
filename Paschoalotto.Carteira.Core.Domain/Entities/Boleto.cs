using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class Boleto
{
    public int Id { get; set; }
    public int AcordoId { get; set; }
    public string NossoNumero { get; set; } = string.Empty;
    public string LinhaDigitavel { get; set; } = string.Empty;
    public string CodigoBarras { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusBoleto Status { get; set; }
    public string? DocumentoPdfBase64 { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos
    public Acordo Acordo { get; set; } = null!;
}
