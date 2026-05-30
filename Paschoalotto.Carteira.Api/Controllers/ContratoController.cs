namespace Paschoalotto.Carteira.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContratoController : ControllerBase
{
    private readonly IContratoService _contratoService;

    public ContratoController(IContratoService contratoService)
    {
        _contratoService = contratoService;
    }

    /// <summary>
    /// Cria um novo contrato de dívida
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ContratoResponseDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] ContratoRequestDto request)
    {
        var contrato = await _contratoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = contrato.Id },
            ApiResponse<ContratoResponseDto>.SuccessResult(contrato, "Contrato criado com sucesso."));
    }

    /// <summary>
    /// Atualiza um contrato existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ContratoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] ContratoRequestDto request)
    {
        var contrato = await _contratoService.UpdateAsync(id, request);
        return Ok(ApiResponse<ContratoResponseDto>.SuccessResult(contrato, "Contrato atualizado com sucesso."));
    }

    /// <summary>
    /// Busca um contrato por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ContratoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var contrato = await _contratoService.GetByIdAsync(id);
        if (contrato == null)
            return NotFound(ApiResponse<ContratoResponseDto>.Failure("Contrato não encontrado."));

        return Ok(ApiResponse<ContratoResponseDto>.SuccessResult(contrato));
    }

    /// <summary>
    /// Lista todos os contratos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContratoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var contratos = await _contratoService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<ContratoResponseDto>>.SuccessResult(contratos));
    }

    /// <summary>
    /// Busca contratos de um cliente específico
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContratoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByClienteId(int clienteId)
    {
        var contratos = await _contratoService.GetByClienteIdAsync(clienteId);
        return Ok(ApiResponse<IEnumerable<ContratoResponseDto>>.SuccessResult(contratos));
    }

    /// <summary>
    /// Busca parcelas de um contrato
    /// </summary>
    [HttpGet("{contratoId}/parcelas")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ParcelaResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetParcelas(int contratoId)
    {
        var parcelas = await _contratoService.GetParcelasByContratoIdAsync(contratoId);
        return Ok(ApiResponse<IEnumerable<ParcelaResponseDto>>.SuccessResult(parcelas));
    }
}
