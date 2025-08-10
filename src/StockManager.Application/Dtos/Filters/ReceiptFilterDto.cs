namespace StockManager.Application.Dtos.Filters;

public class ReceiptFilterDto
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<string>? Numbers { get; set; } = new();
    public List<long>? ResourceIds { get; set; } = new();
    public List<long>? MeasurementUnitIds { get; set; } = new();
}
