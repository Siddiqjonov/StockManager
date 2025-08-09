using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class ReceiptDocumentRepository : IReceiptDocumentRepository
{
    private readonly MainContext _context;

    public ReceiptDocumentRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNumberAsync(string number)
    {
        return await _context.ReceiptDocuments.AnyAsync(r => r.Number == number);
    }

    public async Task<long> AddAsync(ReceiptDocument receiptDocument)
    {
        await _context.ReceiptDocuments.AddAsync(receiptDocument);
        await _context.SaveChangesAsync();
        return receiptDocument.Id;
    }
}
