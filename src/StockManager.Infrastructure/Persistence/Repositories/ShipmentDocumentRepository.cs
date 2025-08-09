using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

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
}
