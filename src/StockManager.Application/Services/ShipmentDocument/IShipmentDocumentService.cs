using StockManager.Application.Dtos.CreateDtos;

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