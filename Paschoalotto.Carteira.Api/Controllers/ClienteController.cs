namespace Paschoalotto.Carteira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Cria um novo cliente (Pessoa Física ou Jurídica)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponseDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ClienteRequestDto request)
    {
        var cliente = await _clienteService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id },
            ApiResponse<ClienteResponseDto>.SuccessResult(cliente, "Cliente criado com sucesso."));
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteRequestDto request)
    {
        var cliente = await _clienteService.UpdateAsync(id, request);
        return Ok(ApiResponse<ClienteResponseDto>.SuccessResult(cliente, "Cliente atualizado com sucesso."));
    }

    /// <summary>
    /// Busca um cliente por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null)
            return NotFound(ApiResponse<ClienteResponseDto>.Failure("Cliente não encontrado."));

        return Ok(ApiResponse<ClienteResponseDto>.SuccessResult(cliente));
    }

    /// <summary>
    /// Busca um cliente por documento (CPF/CNPJ)
    /// </summary>
    [HttpGet("documento/{documento}")]
    [ProducesResponseType(typeof(ApiResponse<ClienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDocumento(string documento)
    {
        var cliente = await _clienteService.GetByDocumentoAsync(documento);
        if (cliente == null)
            return NotFound(ApiResponse<ClienteResponseDto>.Failure("Cliente não encontrado."));

        return Ok(ApiResponse<ClienteResponseDto>.SuccessResult(cliente));
    }

    /// <summary>
    /// Busca dados consolidados de um cliente por documento (CPF/CNPJ) - Dashboard
    /// </summary>
    [HttpGet("documento/{documento}/dashboard")]
    [ProducesResponseType(typeof(ApiResponse<ClienteDashboardDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboardByDocumento(string documento)
    {
        var dashboard = await _clienteService.GetDashboardByDocumentoAsync(documento);
        if (dashboard == null)
            return NotFound(ApiResponse<ClienteDashboardDto>.Failure("Cliente não encontrado."));

        return Ok(ApiResponse<ClienteDashboardDto>.SuccessResult(dashboard, "Dados do cliente carregados com sucesso."));
    }

    /// <summary>
    /// Lista todos os clientes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ClienteResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<ClienteResponseDto>>.SuccessResult(clientes));
    }

    /// <summary>
    /// Pesquisa clientes por nome ou documento
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ClienteResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        var clientes = await _clienteService.SearchAsync(term);
        return Ok(ApiResponse<IEnumerable<ClienteResponseDto>>.SuccessResult(clientes));
    }

    /// <summary>
    /// Remove um cliente
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await _clienteService.DeleteAsync(id);
        return Ok(ApiResponse<bool>.SuccessResult(resultado, "Cliente removido com sucesso."));
    }
}
