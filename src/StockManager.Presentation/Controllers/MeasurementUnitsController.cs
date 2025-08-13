using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Services.Measurement;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeasurementUnitsController : ControllerBase
{
    private readonly IMeasurementUnitService _unitService;

    public MeasurementUnitsController(IMeasurementUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MeasurementUnitFilterDto filter)
    {
        var result = await _unitService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _unitService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MeasurementUnitCreateDto dto)
    {
        var id = await _unitService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] MeasurementUnitUpdateDto dto)
    {
        await _unitService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:long}/archive")]
    public async Task<IActionResult> Archive(long id)
    {
        await _unitService.ArchiveAsync(id);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _unitService.DeleteAsync(id);
        return NoContent();
    }
}
