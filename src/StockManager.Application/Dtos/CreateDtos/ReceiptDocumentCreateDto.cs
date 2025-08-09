using StockManager.Application.Dtos.EnumDtos;

namespace StockManager.Application.Dtos.CreateDtos;

public class ReceiptDocumentCreateDto
{
    public string Number { get; set; } = null!;
    public DateTime Date { get; set; }
    public DocumentStatusDto Status { get; set; } = DocumentStatusDto.Draft;
    public List<ReceiptResourceCreateDto> Resources { get; set; } = new();
}
