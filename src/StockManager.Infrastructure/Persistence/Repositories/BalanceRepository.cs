using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly MainContext _context;

    public BalanceRepository(MainContext context)
    {
        _context = context;
    }

    // Get or create balance row for resource+unit
    public async Task<Balance> GetOrCreateAsync(long resourceId, long measurementUnitId)
    {
        var balance = await _context.Balances
            .FirstOrDefaultAsync(b => b.ResourceId == resourceId && b.MeasurementUnitId == measurementUnitId);

        if (balance == null)
        {
            balance = new Balance
            {
                ResourceId = resourceId,
                MeasurementUnitId = measurementUnitId,
                Quantity = 0m
            };
            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();
        }

        return balance;
    }

    public async Task<decimal> GetQuantityAsync(long resourceId, long measurementUnitId)
    {
        var b = await _context.Balances
            .FirstOrDefaultAsync(x => x.ResourceId == resourceId && x.MeasurementUnitId == measurementUnitId);
        return b?.Quantity ?? 0m;
    }

    public async Task AdjustAsync(long resourceId, long measurementUnitId, decimal delta)
    {
        var b = await GetOrCreateAsync(resourceId, measurementUnitId);
        b.Quantity += delta;
        if (b.Quantity < 0) throw new InvalidOperationException("Количество не может быть отрицательным.");
        _context.Balances.Update(b);
        await _context.SaveChangesAsync();
    }
}
