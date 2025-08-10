using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.Resource;

public interface IResourceService
{
    Task<long> CreateAsync(ResourceCreateDto resourceCreateDto);
    Task ArchiveAsync(long id);
}