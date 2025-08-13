using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.Client;

public interface IClientService
{
    Task<long> CreateAsync(ClientCreateDto clientCreateDto);
    Task ArchiveAsync(long id);
    Task<ClientReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ClientReadDto>> GetAllAsync(ClientFilterDto filter);
    Task UpdateAsync(long id, ClientUpdateDto dto);
    Task DeleteAsync(long id);
}