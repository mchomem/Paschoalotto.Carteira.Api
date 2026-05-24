using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Contrato;

public class ContratoRequestDto
{
    public int ClienteId { get; set; }
    public string NumeroContrato { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public decimal TaxaJurosMensal { get; set; }
    public decimal TaxaMulta { get; set; }
    public decimal TaxaCorrecaoMonetaria { get; set; }
    public DateTime DataContrato { get; set; }
    public DateTime DataVencimento { get; set; }
    public string? Observacoes { get; set; }
}
