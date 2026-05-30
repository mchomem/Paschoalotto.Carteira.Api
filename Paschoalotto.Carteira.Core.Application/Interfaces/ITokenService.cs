namespace Paschoalotto.Carteira.Core.Application.Interfaces;

public interface ITokenService
{
    public Task<TokenResponseDto> GetTokenAsync(ClienteResponseDto cliente);
}
