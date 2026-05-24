namespace Paschoalotto.Carteira.Core.Application.DTOs.Contrato;

public class ParcelaResponseDto
{
    public int Id { get; set; }
    public int NumeroParcela { get; set; }
    public decimal ValorOriginal { get; set; }
    public decimal ValorAtualizado { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? DiasAtraso { get; set; }
}
