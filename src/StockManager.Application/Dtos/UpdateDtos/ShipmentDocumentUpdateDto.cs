using StockManager.Application.Dtos.EnumDtos;

namespace StockManager.Application.Dtos.UpdateDtos;

public class ShipmentDocumentUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; } = null!;
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public DocumentStatusDto Status { get; set; }
    public bool IsSigned { get; set; }
    public List<ShipmentResourceUpdateDto> Resources { get; set; } = new();
}
