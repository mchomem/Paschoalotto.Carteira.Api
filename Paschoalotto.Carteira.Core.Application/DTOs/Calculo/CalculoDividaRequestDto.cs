namespace Paschoalotto.Carteira.Core.Application.DTOs.Calculo;

public class CalculoDividaRequestDto
{
    public int ContratoId { get; set; }
    public DateTime? DataReferencia { get; set; } // Se null, usa data atual
}
