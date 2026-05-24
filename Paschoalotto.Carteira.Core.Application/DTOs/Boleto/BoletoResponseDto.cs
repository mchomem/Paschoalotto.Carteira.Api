using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Boleto;

public class BoletoResponseDto
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
    public string StatusDescricao { get; set; } = string.Empty;
    public DateTime DataEmissao { get; set; }
}
