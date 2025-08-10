using Microsoft.EntityFrameworkCore;
using StockManager.Application.Errors;
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

    public async Task<bool> IsUsedAsync(long id)
    {
        return await _context.ShipmentDocuments.AnyAsync(sd => sd.ClientId == id);
    }

    public Task<Client?> GetClientByIdAsync(long clientId)
    {
        return _context.Clients
            .Include(c => c.ShipmentDocuments)
            .FirstOrDefaultAsync(c => c.Id == clientId);
    }

    public async Task UpdateClientAsync(Client client)
    {
        var clientFromDb = await GetClientByIdAsync(client.Id);
        if (clientFromDb == null)
        {
            throw new NotFoundException($"Клиент с идентификатором: {client.Id} не найден для обновления");
        }
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(long clientId)
    {
        var clientFromDb = await GetClientByIdAsync(clientId) 
            ?? throw new NotFoundException($"Клиент с идентификатором: {clientId} не найден для удаления");
        _context.Clients.Remove(clientFromDb);
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> ExistsByIdAsync(long id)
    {
        return await _context.Clients
            .Include(c => c.ShipmentDocuments)
            .FirstOrDefaultAsync(c => c.Id == id) 
            ?? throw new NotFoundException($"Клиент с идентификатором: {id} не найден");
    }
}
