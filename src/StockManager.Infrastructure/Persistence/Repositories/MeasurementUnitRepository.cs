using Microsoft.EntityFrameworkCore;
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
}
