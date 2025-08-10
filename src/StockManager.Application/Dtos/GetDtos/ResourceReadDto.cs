namespace StockManager.Application.Dtos.GetDtos;

public class ResourceReadDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Status { get; set; } = null!;
}
