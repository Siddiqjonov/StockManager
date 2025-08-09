using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IResourceRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<long> AddAsync(Resource resource);
}
