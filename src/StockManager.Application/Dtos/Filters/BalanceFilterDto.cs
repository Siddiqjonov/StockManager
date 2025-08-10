namespace StockManager.Application.Dtos.Filters;

public class BalanceFilterDto
{
    public List<long>? ResourceIds { get; set; } = new();
    public List<long>? MeasurementUnitIds { get; set; } = new();
}
