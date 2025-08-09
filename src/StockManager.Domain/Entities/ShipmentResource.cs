namespace StockManager.Domain.Entities;

public class ShipmentResource
{
    public long Id { get; set; }
    public long ShipmentDocumentId { get; set; }
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }

    public ShipmentDocument ShipmentDocument { get; set; }
    public Resource Resource { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
}
