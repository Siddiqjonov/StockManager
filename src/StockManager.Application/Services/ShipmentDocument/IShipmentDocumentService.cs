using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.Services.ShipmentDocument;

public interface IShipmentDocumentService
{
    Task<long> CreateAsync(ShipmentDocumentCreateDto dto);
    Task<ShipmentDocumentReadDto?> GetByIdAsync(long id);
    Task<IEnumerable<ShipmentDocumentReadDto>> GetAllAsync(ShipmentFilterDto filter);
    Task UpdateAsync(long id, ShipmentDocumentUpdateDto dto);
    Task DeleteAsync(long id);
    Task SignAsync(long id);
    Task RevokeAsync(long id);
}