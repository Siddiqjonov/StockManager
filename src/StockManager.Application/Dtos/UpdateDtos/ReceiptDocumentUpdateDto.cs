using StockManager.Application.Dtos.EnumDtos;

namespace StockManager.Application.Dtos.UpdateDtos;

public class ReceiptDocumentUpdateDto
{
    public string Number { get; set; } = null!;
    public DateTime Date { get; set; }
    public DocumentStatusDto Status { get; set; }
    public List<ReceiptResourceUpdateDto> Resources { get; set; } = new();
}

