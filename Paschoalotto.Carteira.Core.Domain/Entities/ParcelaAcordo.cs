using Paschoalotto.Carteira.Core.Domain.Enums;

namespace Paschoalotto.Carteira.Core.Domain.Entities;

public class ParcelaAcordo
{
    public int Id { get; set; }
    public int AcordoId { get; set; }
    public int NumeroParcela { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public decimal? ValorPago { get; set; }
    public StatusParcelaAcordo Status { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Relacionamentos
    public Acordo Acordo { get; set; } = null!;
    public Boleto? Boleto { get; set; } // Relacionamento 1:1 opcional
}
