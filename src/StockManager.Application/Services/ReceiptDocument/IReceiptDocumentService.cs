using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.ReceiptDocument;

public interface IReceiptDocumentService
{
    Task<long> CreateAsync(ReceiptDocumentCreateDto dto);
    Task<ReceiptDocumentReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ReceiptDocumentReadDto>> GetAllAsync(ReceiptFilterDto filter);
    Task UpdateAsync(ReceiptDocumentUpdateDto dto);
    Task DeleteAsync(long id);
}