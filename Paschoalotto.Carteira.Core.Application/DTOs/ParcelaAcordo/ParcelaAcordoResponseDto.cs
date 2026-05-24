using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Application.DTOs.ParcelaAcordo;

public class ParcelaAcordoResponseDto
{
    public int Id { get; set; }
    public int AcordoId { get; set; }
    public int NumeroParcela { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusParcelaAcordo Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public bool TemBoleto { get; set; }
    public int? BoletoId { get; set; }
}
