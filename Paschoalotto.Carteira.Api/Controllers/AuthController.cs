namespace Paschoalotto.Carteira.Api.Controllers;

/// <summary>
/// Controlador para autenticação de usuários.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IClienteService _clienteService;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="AuthController"/>.
    /// </summary>
    /// <param name="tokenService">A instância do ITokenService</param>
    /// <param name="clienteService">A instância do IClienteService</param>
    public AuthController(ITokenService tokenService, IClienteService clienteService)
    {
        _tokenService = tokenService;
        _clienteService = clienteService;
    }

    /// <summary>
    /// Autentica o usuário (cliente) e retorna um token JWT para acesso aos recursos protegidos da API.
    /// </summary>
    /// <param name="documento">O documento do cliente (usuário) a ser autenticado CPF ou CNPJ.</param>
    /// <returns>Um token JWT para acesso aos recursos protegidos da API.</returns>
    [HttpGet("login")]
    public async Task<IActionResult> GetLogin([FromQuery] string documento)
    {
        var cliente = await _clienteService.GetByDocumentoAsync(documento);
        var tokenResponse = await _tokenService.GetTokenAsync(cliente);
        tokenResponse.UserId = cliente.Id;

        var response = ApiResponse<TokenResponseDto>.SuccessResult(tokenResponse);
        return Ok(response);
    }
}
