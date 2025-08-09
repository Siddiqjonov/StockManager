using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;
using StockManager.Application.Errors;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class ShipmentDocumentRepository : IShipmentDocumentRepository
{
    private readonly MainContext _context;

    public ShipmentDocumentRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNumberAsync(string number)
    {
        return await _context.ShipmentDocuments.AnyAsync(s => s.Number == number);
    }

    public async Task<long> AddAsync(ShipmentDocument shipmentDocument)
    {
        await _context.ShipmentDocuments.AddAsync(shipmentDocument);
        await _context.SaveChangesAsync();
        return shipmentDocument.Id;
    }

    public async Task<ShipmentDocument?> GetShipmentDocumentByIdWithResourcesAsync(long id)
    {
        var ShipmentDocument = await _context.ShipmentDocuments
            .Include(s => s.Resources)
            .FirstOrDefaultAsync(s => s.Id == id);
        return ShipmentDocument;
    }

    public async Task<ShipmentDocument> GetShipmentDocumentByIdAsync(long shipmentDocumentId)
    {
        var shipmentDocument = await _context.ShipmentDocuments
            .FirstOrDefaultAsync(s => s.Id == shipmentDocumentId);
        return shipmentDocument
            ?? throw new NotFoundException($"Документ об отгрузке с идентификатором: {shipmentDocumentId} не найден");
    }

    public async Task UpdateAsync(ShipmentDocument shipmentDocument)
    {
        await GetShipmentDocumentByIdAsync(shipmentDocument.Id);
        _context.ShipmentDocuments.Update(shipmentDocument);
        await _context.SaveChangesAsync();
    }
}
