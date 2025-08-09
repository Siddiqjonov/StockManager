using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IMeasurementUnitRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<long> AddAsync(MeasurementUnit measurementUnit);
    Task<bool> IsUsedAsync(long id);
    Task<MeasurementUnit?> GetMeasurementUnitByIdAsync(long measurementUnitId);
    Task UpdateMeasurementUnitAsync(MeasurementUnit measurementUnit);
    Task DeleteMeasurementUnitAsync(long measurementUnitId);
}
