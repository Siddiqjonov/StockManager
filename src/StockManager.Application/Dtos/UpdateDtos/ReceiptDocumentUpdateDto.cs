using StockManager.Application.Dtos.EnumDtos;

namespace StockManager.Application.Dtos.UpdateDtos;

public class ReceiptDocumentUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; } = null!;
    public long SupplierId { get; set; }
    public DateTime Date { get; set; }
    public List<ReceiptResourceUpdateDto> Resources { get; set; } = new();
}
