using Paschoalotto.Carteira.Core.Application.DTOs.Boleto;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IBoletoService
{
    Task<BoletoResponseDto> GerarBoletoAsync(BoletoRequestDto request);
    Task<BoletoPdfResponseDto> GerarBoletoPdfAsync(int boletoId);
    Task<IEnumerable<BoletoResponseDto>> GetByAcordoIdAsync(int acordoId);
    Task<BoletoResponseDto?> GetByIdAsync(int id);
    Task<BoletoResponseDto?> GetByNossoNumeroAsync(string nossoNumero);
    Task<bool> CancelarBoletoAsync(int boletoId);
}
