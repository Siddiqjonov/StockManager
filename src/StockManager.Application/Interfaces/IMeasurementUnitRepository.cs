using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IMeasurementUnitRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<long> AddAsync(MeasurementUnit measurementUnit);
}
