using Paschoalotto.Carteira.Core.Application.DTOs.Cliente;

namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface IClienteService
{
    Task<ClienteResponseDto> CreateAsync(ClienteRequestDto request);
    Task<ClienteResponseDto> UpdateAsync(int id, ClienteRequestDto request);
    Task<ClienteResponseDto?> GetByIdAsync(int id);
    Task<ClienteResponseDto?> GetByDocumentoAsync(string documento);
    Task<IEnumerable<ClienteResponseDto>> GetAllAsync();
    Task<IEnumerable<ClienteResponseDto>> SearchAsync(string searchTerm);
    Task<bool> DeleteAsync(int id);
}
