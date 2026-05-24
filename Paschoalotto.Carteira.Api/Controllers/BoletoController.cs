namespace Paschoalotto.Carteira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoletoController : ControllerBase
{
    private readonly IBoletoService _boletoService;

    public BoletoController(IBoletoService boletoService)
    {
        _boletoService = boletoService;
    }

    /// <summary>
    /// Gera um boleto bancário para um acordo
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<BoletoResponseDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GerarBoleto([FromBody] BoletoRequestDto request)
    {
        var boleto = await _boletoService.GerarBoletoAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = boleto.Id },
            ApiResponse<BoletoResponseDto>.SuccessResult(boleto, "Boleto gerado com sucesso."));
    }

    /// <summary>
    /// Gera o PDF de um boleto existente
    /// </summary>
    [HttpGet("{id}/pdf")]
    [ProducesResponseType(typeof(ApiResponse<BoletoPdfResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GerarPdf(int id)
    {
        var boletoPdf = await _boletoService.GerarBoletoPdfAsync(id);
        return Ok(ApiResponse<BoletoPdfResponseDto>.SuccessResult(boletoPdf, "PDF do boleto gerado com sucesso."));
    }

    /// <summary>
    /// Busca um boleto por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<BoletoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var boleto = await _boletoService.GetByIdAsync(id);
        if (boleto == null)
            return NotFound(ApiResponse<BoletoResponseDto>.Failure("Boleto não encontrado."));

        return Ok(ApiResponse<BoletoResponseDto>.SuccessResult(boleto));
    }

    /// <summary>
    /// Busca boletos de um acordo específico
    /// </summary>
    [HttpGet("acordo/{acordoId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<BoletoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAcordoId(int acordoId)
    {
        var boletos = await _boletoService.GetByAcordoIdAsync(acordoId);
        return Ok(ApiResponse<IEnumerable<BoletoResponseDto>>.SuccessResult(boletos));
    }

    /// <summary>
    /// Cancela um boleto
    /// </summary>
    [HttpPost("{id}/cancelar")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _boletoService.CancelarBoletoAsync(id);
        return Ok(ApiResponse<bool>.SuccessResult(resultado, "Boleto cancelado com sucesso."));
    }
}
