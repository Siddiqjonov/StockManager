namespace StockManager.Application.Dtos.UpdateDtos;

public class ClientUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
