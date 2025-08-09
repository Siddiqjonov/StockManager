using StockManager.Application.Dtos.EnumDtos;

namespace StockManager.Application.Dtos.CreateDtos;

public class ShipmentDocumentCreateDto
{
    public string Number { get; set; } = null!;
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public DocumentStatusDto Status { get; set; } = DocumentStatusDto.Draft;
    public bool IsSigned { get; set; } = false;
    public List<ShipmentResourceCreateDto> Resources { get; set; } = new();
}
