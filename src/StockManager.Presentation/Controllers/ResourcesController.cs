using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Services.Resource;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ResourceFilterDto filter)
    {
        var result = await _resourceService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _resourceService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ResourceCreateDto dto)
    {
        var id = await _resourceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] ResourceUpdateDto dto)
    {
        await _resourceService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("{id:long}/archive")]
    public async Task<IActionResult> Archive(long id)
    {
        await _resourceService.ArchiveAsync(id);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _resourceService.DeleteAsync(id);
        return NoContent();
    }
}
