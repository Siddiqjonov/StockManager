namespace StockManager.Application.Dtos.UpdateDtos;

public class ResourceUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long MeasurementUnitId { get; set; }
}
