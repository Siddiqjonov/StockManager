using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IClientRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<long> AddAsync(Client client);
    Task<bool> IsUsedAsync(long id);
}
