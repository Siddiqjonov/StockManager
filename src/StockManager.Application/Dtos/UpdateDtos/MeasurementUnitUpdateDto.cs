namespace StockManager.Application.Dtos.UpdateDtos;

public class MeasurementUnitUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;
}
