using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IReceiptDocumentRepository
{
    Task<long> AddAsync(ReceiptDocument receiptDocument);
    Task<bool> ExistsByNumberAsync(string number);
}
