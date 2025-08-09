using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.ReceiptDocument;

public interface IReceiptDocumentService
{
    Task<long> CreateAsync(ReceiptDocumentCreateDto receiptDocumentCreateDto);
}