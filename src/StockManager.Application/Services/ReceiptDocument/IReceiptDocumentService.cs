using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.ReceiptDocument;

public interface IReceiptDocumentService
{
    Task<long> CreateAsync(ReceiptDocumentCreateDto dto);
    Task DeleteAsync(long id);
    Task UpdateAsync(long id, ReceiptDocumentUpdateDto dto);
}