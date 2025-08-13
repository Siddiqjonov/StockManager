using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Services.ShipmentDocument;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipmentsController : ControllerBase
{
    private readonly IShipmentDocumentService _shipmentService;

    public ShipmentsController(IShipmentDocumentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ShipmentFilterDto filter)
    {
        var result = await _shipmentService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _shipmentService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShipmentDocumentCreateDto dto)
    {
        var id = await _shipmentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] ShipmentDocumentUpdateDto dto)
    {
        await _shipmentService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:long}/sign")]
    public async Task<IActionResult> Sign(long id)
    {
        await _shipmentService.SignAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:long}/revoke")]
    public async Task<IActionResult> Revoke(long id)
    {
        await _shipmentService.RevokeAsync(id);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _shipmentService.DeleteAsync(id);
        return NoContent();
    }
}
