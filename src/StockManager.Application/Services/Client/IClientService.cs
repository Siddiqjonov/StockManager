using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.Client;

public interface IClientService
{
    Task<long> CreateAsync(ClientCreateDto clientCreateDto);
    Task ArchiveAsync(long id);
}