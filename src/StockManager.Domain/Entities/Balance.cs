namespace StockManager.Domain.Entities;

public class Balance
{
    public long Id { get; set; }
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }

    public Resource Resource { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
}
