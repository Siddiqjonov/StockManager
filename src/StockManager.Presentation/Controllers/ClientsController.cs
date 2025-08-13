using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Services.Client;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ClientFilterDto filter)
    {
        var result = await _clientService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _clientService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateDto dto)
    {
        var id = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] ClientUpdateDto dto)
    {
        await _clientService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:long}/archive")]
    public async Task<IActionResult> Archive(long id)
    {
        await _clientService.ArchiveAsync(id);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _clientService.DeleteAsync(id);
        return NoContent();
    }
}
