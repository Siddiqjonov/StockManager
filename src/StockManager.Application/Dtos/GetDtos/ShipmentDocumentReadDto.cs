namespace StockManager.Application.Dtos.GetDtos;

public class ShipmentDocumentReadDto
{
    public long Id { get; set; }
    public string Number { get; set; } = null!;
    public long ClientId { get; set; }
    public string ClientName { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Status { get; set; } = null!;
    public bool IsSigned { get; set; }
    public List<ShipmentResourceReadDto> Resources { get; set; } = new();
}
