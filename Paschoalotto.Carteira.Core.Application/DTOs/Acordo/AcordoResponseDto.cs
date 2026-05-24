using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Acordo;

public class AcordoResponseDto
{
    public int Id { get; set; }
    public int ContratoId { get; set; }
    public string NumeroAcordo { get; set; } = string.Empty;
    public string NumeroContrato { get; set; } = string.Empty;
    public string ClienteNome { get; set; } = string.Empty;
    public decimal ValorTotalDivida { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal PercentualDesconto { get; set; }
    public decimal ValorTotalAcordo { get; set; }
    public decimal? ValorEntrada { get; set; }
    public int QuantidadeParcelas { get; set; }
    public decimal ValorParcela { get; set; }
    public DateTime DataPrimeiroVencimento { get; set; }
    public DateTime DataAcordo { get; set; }
    public StatusAcordo Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
}
