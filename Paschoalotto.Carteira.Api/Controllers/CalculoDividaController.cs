namespace Paschoalotto.Carteira.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculoDividaController : ControllerBase
{
    private readonly IDebtCalculationService _debtCalculationService;

    public CalculoDividaController(IDebtCalculationService debtCalculationService)
    {
        _debtCalculationService = debtCalculationService;
    }

    /// <summary>
    /// Calcula a dívida atualizada de um contrato com juros, multa e correção
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CalculoDividaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalcularDivida([FromBody] CalculoDividaRequestDto request)
    {
        var calculo = await _debtCalculationService.CalcularDividaAsync(request);
        return Ok(ApiResponse<CalculoDividaResponseDto>.SuccessResult(calculo, "Cálculo realizado com sucesso."));
    }

    /// <summary>
    /// Calcula a dívida atualizada de um contrato específico
    /// </summary>
    [HttpGet("{contratoId}")]
    [ProducesResponseType(typeof(ApiResponse<CalculoDividaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalcularDividaByContrato(int contratoId, [FromQuery] DateTime? dataReferencia = null)
    {
        var calculo = await _debtCalculationService.CalcularDividaByContratoIdAsync(contratoId, dataReferencia);
        return Ok(ApiResponse<CalculoDividaResponseDto>.SuccessResult(calculo, "Cálculo realizado com sucesso."));
    }
}
