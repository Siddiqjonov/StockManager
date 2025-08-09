using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IReceiptDocumentRepository
{
    Task<long> AddAsync(ReceiptDocument receiptDocument);
    Task<bool> ExistsByNumberAsync(string number);
    Task<ReceiptDocument?> GetByIdWithResourcesAsync(long id);
    Task<ReceiptDocument> GetByIdAsync(long receiptDocumentId);
    Task DeleteAsync(long receiptDocumentId);
    Task UpdateReceiptDocumentAsync(ReceiptDocument receiptDocument);
}
