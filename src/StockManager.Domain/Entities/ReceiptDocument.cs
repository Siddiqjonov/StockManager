namespace StockManager.Domain.Entities;

public class ReceiptDocument
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }

    public ICollection<ReceiptResource> Resources { get; set; }
}
