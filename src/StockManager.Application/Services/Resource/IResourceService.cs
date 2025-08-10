using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.Resource;

public interface IResourceService
{
    Task<long> CreateAsync(ResourceCreateDto resourceCreateDto);
    Task ArchiveAsync(long id);
    Task<ResourceReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ResourceReadDto>> GetAllAsync(ResourceFilterDto filter);
    Task UpdateAsync(long id, ResourceUpdateDto dto);
    Task DeleteAsync(long id);      // hard delete (if allowed) or throws; service may archive if in use
}