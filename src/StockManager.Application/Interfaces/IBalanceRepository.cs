using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IBalanceRepository
{
    Task AdjustAsync(long resourceId, long measurementUnitId, decimal delta);
    Task<decimal> GetQuantityAsync(long resourceId, long measurementUnitId);
    Task<Balance> GetOrCreateAsync(long resourceId, long measurementUnitId);
}
