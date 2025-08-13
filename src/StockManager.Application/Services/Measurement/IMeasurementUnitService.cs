using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.Measurement;

public interface IMeasurementUnitService
{
    Task<long> CreateAsync(MeasurementUnitCreateDto measurementUnitCreateDto);
    Task ArchiveAsync(long id);
    Task<MeasurementUnitReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<MeasurementUnitReadDto>> GetAllAsync(MeasurementUnitFilterDto filter);
    Task UpdateAsync(long id, MeasurementUnitUpdateDto dto);
    Task DeleteAsync(long id);
}