using Paschoalotto.Carteira.Core.Application.DTOs.Acordo;
using Paschoalotto.Carteira.Core.Application.DTOs.Boleto;
using Paschoalotto.Carteira.Core.Application.DTOs.Contrato;
using Paschoalotto.Carteira.Core.Application.DTOs.ParcelaAcordo;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Cliente;

public class ClienteDashboardDto
{
    public ClienteResponseDto Cliente { get; set; } = null!;
    public List<ContratoDashboardDto> Contratos { get; set; } = new();
    public List<ParcelaResponseDto> Parcelas { get; set; } = new();
    public List<AcordoResponseDto> Acordos { get; set; } = new();
    public List<ParcelaAcordoResponseDto> ParcelasAcordo { get; set; } = new();
    public List<BoletoResponseDto> Boletos { get; set; } = new();
    public decimal TotalDivida { get; set; }
    public int TotalParcelas { get; set; }
    public int ParcelasVencidas { get; set; }
    public DateTime? ProximoVencimento { get; set; }
}

public class ContratoDashboardDto
{
    public ContratoResponseDto Contrato { get; set; } = null!;
    public List<ParcelaResponseDto> ParcelasEmAberto { get; set; } = new();
    public AcordoResponseDto? AcordoAtivo { get; set; }
    public List<BoletoResponseDto> BoletosPendentes { get; set; } = new();
}
