namespace StockManager.Application.Dtos.GetDtos;

public class ReceiptDocumentReadDto
{
    public long Id { get; set; }
    public string Number { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Status { get; set; } = null!;
    public List<ReceiptResourceReadDto> Resources { get; set; } = new();
}
