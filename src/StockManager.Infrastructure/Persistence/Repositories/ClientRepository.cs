using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly MainContext _context;

    public ClientRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Clients.AnyAsync(c => c.Name == name);
    }

    public async Task<long> AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client.Id;
    }
}
