using Microsoft.EntityFrameworkCore;
using StockManager.Application.Errors;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using StockManager.Infrastructure.Persistence.DataContext;

namespace StockManager.Infrastructure.Persistence.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly MainContext _context;

    public ResourceRepository(MainContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Resources.AnyAsync(r => r.Name == name);
    }

    public async Task<long> AddAsync(Resource resource)
    {
        await _context.Resources.AddAsync(resource);
        await _context.SaveChangesAsync();
        return resource.Id;
    }

    public async Task<bool> IsUsedAsync(long resourceId)
    {
        return await _context.Balances.AnyAsync(b => b.ResourceId == resourceId)
        || await _context.ReceiptResources.AnyAsync(rr => rr.ResourceId == resourceId)
        || await _context.ShipmentResources.AnyAsync(sr => sr.ResourceId == resourceId);
    }

    public async Task<Resource?> GetByIdAsync(long id)
    {
        return await _context.Resources
            .Include(r => r.Balances)
            .Include(r => r.ReceiptResources)
            .Include(r => r.ShipmentResources)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateResouceAsync(Resource resource)
    {
        var resourceFromDb = await GetByIdAsync(resource.Id);
        if (resourceFromDb != null)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
        }
        else
            throw new NotFoundException($"Ресурс с идентификатором: {resource.Id} не найден для обновления");
    }

    public async Task DeleteResourceAsync(long resourceId)
    {
        var resourceFromDb = await GetByIdAsync(resourceId);
        if (resourceFromDb != null)
        {
            _context.Resources.Remove(resourceFromDb);
            await _context.SaveChangesAsync();
        }
        else
            throw new NotFoundException($"Ресурс с идентификатором: {resourceId} не найден для удаления");
    }
}
