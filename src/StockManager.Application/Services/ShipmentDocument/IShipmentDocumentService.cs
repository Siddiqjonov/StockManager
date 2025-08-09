using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.Services.ShipmentDocument;

public interface IShipmentDocumentService
{
    Task<long> CreateAsync(ShipmentDocumentCreateDto shipmentDocumentCreateDto);
}