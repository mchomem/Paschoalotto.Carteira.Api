namespace Paschoalotto.Carteira.Core.Application.DTOs.Boleto;

public class BoletoPdfResponseDto
{
    public int BoletoId { get; set; }
    public string NossoNumero { get; set; } = string.Empty;
    public string DocumentoPdfBase64 { get; set; } = string.Empty;
    public string ContentType { get; set; } = "application/pdf";
    public string FileName { get; set; } = string.Empty;
}
