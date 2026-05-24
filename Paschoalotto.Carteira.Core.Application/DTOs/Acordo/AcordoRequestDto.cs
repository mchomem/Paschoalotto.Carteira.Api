namespace Paschoalotto.Carteira.Core.Application.DTOs.Acordo;

public class AcordoRequestDto
{
    public int ContratoId { get; set; }
    public decimal? ValorDesconto { get; set; }
    public decimal? PercentualDesconto { get; set; }
    public decimal? ValorEntrada { get; set; }
    public int QuantidadeParcelas { get; set; }
    public DateTime DataPrimeiroVencimento { get; set; }
    public string? Observacoes { get; set; }
}
