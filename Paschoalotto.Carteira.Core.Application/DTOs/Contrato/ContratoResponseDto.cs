using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Contrato;

public class ContratoResponseDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string NumeroContrato { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public decimal SaldoDevedor { get; set; }
    public decimal TaxaJurosMensal { get; set; }
    public decimal TaxaMulta { get; set; }
    public decimal TaxaCorrecaoMonetaria { get; set; }
    public DateTime DataContrato { get; set; }
    public DateTime DataVencimento { get; set; }
    public StatusContrato Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
    public DateTime DataCadastro { get; set; }
}
