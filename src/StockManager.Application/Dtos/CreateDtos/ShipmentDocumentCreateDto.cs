namespace StockManager.Application.Dtos.CreateDtos;

public class ShipmentDocumentCreateDto
{
    public string Number { get; set; }
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public bool IsSigned { get; set; } = false;
}
