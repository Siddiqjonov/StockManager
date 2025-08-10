namespace StockManager.Application.Dtos.GetDtos;

public class ClientReadDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Status { get; set; } = null!;
}
