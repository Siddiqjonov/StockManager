using StockManager.Domain.Entities;

namespace StockManager.Application.Interfaces;

public interface IShipmentDocumentRepository
{
    Task<long> AddAsync(ShipmentDocument shipmentDocument);
    Task<bool> ExistsByNumberAsync(string number);
}
