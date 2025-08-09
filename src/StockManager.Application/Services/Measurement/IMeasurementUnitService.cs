using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.Measurement;

public interface IMeasurementUnitService
{
    Task<long> CreateAsync(MeasurementUnitCreateDto measurementUnitCreateDto);
}