using Paschoalotto.Carteira.Core.Application.DTOs.Acordo;
using Paschoalotto.Carteira.Core.Application.DTOs.Boleto;
using Paschoalotto.Carteira.Core.Application.DTOs.Contrato;

namespace Paschoalotto.Carteira.Core.Application.DTOs.Cliente;

public class ClienteDashboardDto
{
    public ClienteResponseDto Cliente { get; set; } = null!;
    public List<ContratoDashboardDto> Contratos { get; set; } = new();
}

public class ContratoDashboardDto
{
    public ContratoResponseDto Contrato { get; set; } = null!;
    public List<ParcelaResponseDto> ParcelasEmAberto { get; set; } = new();
    public AcordoResponseDto? AcordoAtivo { get; set; }
    public List<BoletoResponseDto> BoletosPendentes { get; set; } = new();
}
