using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.ReceiptDocument;

public interface IReceiptDocumentService
{
    Task<long> CreateAsync(ReceiptDocumentCreateDto dto);
    Task<ReceiptDocumentReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ReceiptDocumentReadDto>> GetAllAsync(ReceiptFilterDto filter);
    Task UpdateAsync(long id, ReceiptDocumentUpdateDto dto);
    Task DeleteAsync(long id);
}