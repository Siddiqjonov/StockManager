namespace StockManager.Application.Dtos.UpdateDtos;

public class ReceiptResourceUpdateDto
{
    public long ResourceId { get; set; }
    public long MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }
}
