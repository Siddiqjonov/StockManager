using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IResourceRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<long> AddAsync(Resource resource);
    Task<bool> IsUsedAsync(long resourceId);
    Task<Resource?> GetByIdAsync(long id);
    Task UpdateResouceAsync(Resource resource);
    Task DeleteResourceAsync(long resourceId);
}
