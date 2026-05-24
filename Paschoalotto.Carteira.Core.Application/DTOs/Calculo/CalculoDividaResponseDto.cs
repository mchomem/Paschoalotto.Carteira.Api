namespace Paschoalotto.Carteira.Core.Application.DTOs.Calculo;

public class CalculoDividaResponseDto
{
    public int ContratoId { get; set; }
    public string NumeroContrato { get; set; } = string.Empty;
    public DateTime DataCalculo { get; set; }
    public decimal ValorOriginalContrato { get; set; }
    public decimal SaldoDevedorAtualizado { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal ValorMulta { get; set; }
    public decimal ValorCorrecaoMonetaria { get; set; }
    public int QuantidadeParcelasAbertas { get; set; }
    public int QuantidadeParcelasVencidas { get; set; }
    public List<ParcelaCalculoDto> ParcelasEmAberto { get; set; } = new();
    public string Observacoes { get; set; } = string.Empty;
}

public class ParcelaCalculoDto
{
    public int ParcelaId { get; set; }
    public int NumeroParcela { get; set; }
    public decimal ValorOriginal { get; set; }
    public decimal ValorAtualizado { get; set; }
    public DateTime DataVencimento { get; set; }
    public int DiasAtraso { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal ValorMulta { get; set; }
    public decimal ValorCorrecao { get; set; }
}
