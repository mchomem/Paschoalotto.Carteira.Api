namespace Paschoalotto.Carteira.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AcordoController : ControllerBase
{
    private readonly IAgreementService _agreementService;

    public AcordoController(IAgreementService agreementService)
    {
        _agreementService = agreementService;
    }

    /// <summary>
    /// Cria um novo acordo de negociação de dívida
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AcordoResponseDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] AcordoRequestDto request)
    {
        var acordo = await _agreementService.CreateAcordoAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = acordo.Id },
            ApiResponse<AcordoResponseDto>.SuccessResult(acordo, "Acordo criado com sucesso."));
    }

    /// <summary>
    /// Busca um acordo por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AcordoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var acordo = await _agreementService.GetByIdAsync(id);
        if (acordo == null)
            return NotFound(ApiResponse<AcordoResponseDto>.Failure("Acordo não encontrado."));

        return Ok(ApiResponse<AcordoResponseDto>.SuccessResult(acordo));
    }

    /// <summary>
    /// Lista todos os acordos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AcordoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var acordos = await _agreementService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<AcordoResponseDto>>.SuccessResult(acordos));
    }

    /// <summary>
    /// Busca acordos de um contrato específico
    /// </summary>
    [HttpGet("contrato/{contratoId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<AcordoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByContratoId(int contratoId)
    {
        var acordos = await _agreementService.GetByContratoIdAsync(contratoId);
        return Ok(ApiResponse<IEnumerable<AcordoResponseDto>>.SuccessResult(acordos));
    }

    /// <summary>
    /// Cancela um acordo ativo
    /// </summary>
    [HttpPost("{id}/cancelar")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _agreementService.CancelarAcordoAsync(id);
        return Ok(ApiResponse<bool>.SuccessResult(resultado, "Acordo cancelado com sucesso."));
    }
}
