using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IShipmentDocumentRepository
{
    Task<long> AddAsync(ShipmentDocument shipmentDocument);
    Task<bool> ExistsByNumberAsync(string number);
    Task UpdateAsync(ShipmentDocument shipmentDocument);
    Task<ShipmentDocument> GetShipmentDocumentByIdAsync(long shipmentDocumentId);
    Task<ShipmentDocument?> GetShipmentDocumentByIdWithResourcesAsync(long id);
}
