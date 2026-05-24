using Paschoalotto.Carteira.Core.Application.DTOs.Contrato;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IContratoService
{
    Task<ContratoResponseDto> CreateAsync(ContratoRequestDto request);
    Task<ContratoResponseDto> UpdateAsync(int id, ContratoRequestDto request);
    Task<ContratoResponseDto?> GetByIdAsync(int id);
    Task<ContratoResponseDto?> GetByNumeroContratoAsync(string numeroContrato);
    Task<IEnumerable<ContratoResponseDto>> GetByClienteIdAsync(int clienteId);
    Task<IEnumerable<ContratoResponseDto>> GetAllAsync();
    Task<IEnumerable<ParcelaResponseDto>> GetParcelasByContratoIdAsync(int contratoId);
    Task<bool> DeleteAsync(int id);
}
