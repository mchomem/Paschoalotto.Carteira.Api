namespace Paschoalotto.Carteira.Core.Application.DTOs.Auth;

public class TokenResponseDto
{
    public int UserId { get; set; }
    public string? Value { get; set; }
    public DateTime? ExpiresIn { get; set; }
}
