using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.Resource;

public interface IResourceService
{
    Task<long> CreateAsync(ResourceCreateDto resourceCreateDto);
    Task ArchiveAsync(long id);
    Task<ResourceReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ResourceReadDto>> GetAllAsync(ResourceFilterDto filter);
    Task UpdateAsync(ResourceUpdateDto dto);
    Task DeleteAsync(long id);
}