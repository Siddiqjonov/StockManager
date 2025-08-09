namespace StockManager.Application.Dtos.CreateDtos;

public class ClientCreateDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; } = true;
}
