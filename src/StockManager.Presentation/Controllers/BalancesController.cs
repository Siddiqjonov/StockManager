using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Services.Balance;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BalancesController : ControllerBase
{
    private readonly IBalanceService _balanceService;

    public BalancesController(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BalanceFilterDto filter)
    {
        var result = await _balanceService.GetAllAsync(filter);
        return Ok(result);
    }
}
