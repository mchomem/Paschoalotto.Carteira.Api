namespace Paschoalotto.Carteira.Core.Application.Services;

public class TokenService : ITokenService
{
    private readonly byte[] key = Encoding.ASCII.GetBytes("+WsLhdwMcCnW&cJW4a5hm^jFemE&?V?Y?z9eMdcN_X3DktLE7W9nS#Z2&vpakM6v");

    public async Task<TokenResponseDto> GetTokenAsync(ClienteResponseDto cliente)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(nameof(cliente.Id).ToLower(), cliente.Id.ToString()),
                new Claim("startedIn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"))
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string generatedToken = string.Empty;
        await Task.Run(() => generatedToken = tokenHandler.WriteToken(token));
        var jwtToken = tokenHandler.ReadJwtToken(generatedToken);

        return new TokenResponseDto()
        {
            Value = generatedToken,
            ExpiresIn = jwtToken.ValidTo
        };
    }
}
