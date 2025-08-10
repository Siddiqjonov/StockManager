namespace StockManager.Application.Dtos.GetDtos;

public class ShipmentResourceReadDto
{
    public long ResourceId { get; set; }
    public string ResourceName { get; set; } = null!;
    public long MeasurementUnitId { get; set; }
    public string MeasurementUnitName { get; set; } = null!;
    public decimal Quantity { get; set; }
}
