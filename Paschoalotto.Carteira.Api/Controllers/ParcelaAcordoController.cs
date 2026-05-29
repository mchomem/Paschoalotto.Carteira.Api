namespace Paschoalotto.Carteira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParcelaAcordoController : ControllerBase
{
    private readonly IParcelaAcordoService _parcelaAcordoService;

    public ParcelaAcordoController(IParcelaAcordoService parcelaAcordoService)
    {
        _parcelaAcordoService = parcelaAcordoService;
    }

    /// <summary>
    /// Busca uma parcela de acordo por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ParcelaAcordoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var parcela = await _parcelaAcordoService.GetByIdAsync(id);
        if (parcela == null)
            return NotFound(ApiResponse<ParcelaAcordoResponseDto>.Failure("Parcela não encontrada."));

        return Ok(ApiResponse<ParcelaAcordoResponseDto>.SuccessResult(parcela));
    }

    /// <summary>
    /// Busca todas as parcelas de um acordo específico
    /// </summary>
    [HttpGet("acordo/{acordoId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ParcelaAcordoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAcordoId(int acordoId)
    {
        var parcelas = await _parcelaAcordoService.GetByAcordoIdAsync(acordoId);
        return Ok(ApiResponse<IEnumerable<ParcelaAcordoResponseDto>>.SuccessResult(parcelas));
    }

    /// <summary>
    /// Lista todas as parcelas vencidas
    /// </summary>
    [HttpGet("vencidas")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ParcelaAcordoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetParcelasVencidas()
    {
        var parcelas = await _parcelaAcordoService.GetParcelasVencidasAsync();
        return Ok(ApiResponse<IEnumerable<ParcelaAcordoResponseDto>>.SuccessResult(parcelas));
    }
}
