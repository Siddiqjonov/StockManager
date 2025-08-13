using Microsoft.EntityFrameworkCore;
using StockManager.Application.Errors;
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

    public async Task<ReceiptDocument?> GetByIdWithResourcesAsync(long id)
    {
        return await _context.ReceiptDocuments.Include(r => r.Resources).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ReceiptDocument> GetByIdAsync(long receiptDocumentId)
    {
        return await _context.ReceiptDocuments.FirstOrDefaultAsync(rD => rD.Id == receiptDocumentId)
            ?? throw new NotFoundException($"Документ о получении с идентификатором {receiptDocumentId} не найден");
    }

    public async Task DeleteAsync(long receiptDocumentId)
    {
        var receiptDocument = await GetByIdAsync(receiptDocumentId);
        _context.ReceiptDocuments.Remove(receiptDocument);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReceiptDocumentAsync(ReceiptDocument receiptDocument)
    {
        await GetByIdAsync(receiptDocument.Id);
        _context.ReceiptDocuments.Update(receiptDocument);
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<ReceiptDocument>> GetAllWithResourcesAsync()
    {
        return _context.ReceiptDocuments
            .Include(r => r.Resources)
            .AsNoTracking()
            .ToListAsync()
            .ContinueWith(task => (IEnumerable<ReceiptDocument>)task.Result);
    }
}
