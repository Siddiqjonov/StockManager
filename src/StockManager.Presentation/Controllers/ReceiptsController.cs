using Microsoft.AspNetCore.Mvc;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Services.ReceiptDocument;

namespace StockManager.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReceiptsController : ControllerBase
{
    private readonly IReceiptDocumentService _receiptService;

    public ReceiptsController(IReceiptDocumentService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ReceiptFilterDto filter)
    {
        var result = await _receiptService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _receiptService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReceiptDocumentCreateDto dto)
    {
        var id = await _receiptService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] ReceiptDocumentUpdateDto dto)
    {
        await _receiptService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _receiptService.DeleteAsync(id);
        return NoContent();
    }
}
