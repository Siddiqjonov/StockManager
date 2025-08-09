namespace StockManager.Application.Dtos.CreateDtos;

public class ShipmentResourceCreateDto
{
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }
}
