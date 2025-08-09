using Microsoft.EntityFrameworkCore;
using StockManager.Application.Errors;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class MeasurementUnitRepository : IMeasurementUnitRepository
{
    private readonly MainContext _context;

    public MeasurementUnitRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.MeasurementUnits.AnyAsync(m => m.Name == name);
    }

    public async Task<long> AddAsync(MeasurementUnit measurementUnit)
    {
        await _context.MeasurementUnits.AddAsync(measurementUnit);
        await _context.SaveChangesAsync();
        return measurementUnit.Id;
    }
    public async Task<bool> IsUsedAsync(long id)
    {
        var isUsed = 
            await _context.Balances.AnyAsync(b => b.MeasurementUnitId == id)
        || await _context.ReceiptResources.AnyAsync(rr => rr.MeasurementUnitId == id)
        || await _context.ShipmentResources.AnyAsync(sr => sr.MeasurementUnitId == id);
        return isUsed;
    }

    public async Task<MeasurementUnit?> GetMeasurementUnitByIdAsync(long measurementUnitId)
    {
        return await _context.MeasurementUnits
            .Include(mu => mu.Balances)
            .Include(mu => mu.ReceiptResources)
            .Include(mu => mu.ShipmentResources)
            .FirstOrDefaultAsync(mu => mu.Id == measurementUnitId);
    }

    public async Task UpdateMeasurementUnitAsync(MeasurementUnit measurementUnit)
    {
        var measurementUnitFromDb = await GetMeasurementUnitByIdAsync(measurementUnit.Id);
        if (measurementUnitFromDb != null)
        {
            _context.MeasurementUnits.Update(measurementUnit);
            await _context.SaveChangesAsync();
        }
        else
            throw new NotFoundException($"Единица измерения с идентификатором: {measurementUnit.Id} не найдена для обновления");
    }

    public async Task DeleteMeasurementUnitAsync(long measurementUnitId)
    {
        var measurementUnitFromDb = await GetMeasurementUnitByIdAsync(measurementUnitId);
        if (measurementUnitFromDb != null)
        {
            _context.MeasurementUnits.Remove(measurementUnitFromDb);
            await _context.SaveChangesAsync();
        }
        else
            throw new NotFoundException($"Единица измерения с идентификатором: {measurementUnitId} не найдена для удаления");
    }
}
