using StockManager.Domain.Enums;

namespace StockManager.Domain.Entities;

public class ShipmentDocument
{
    public long Id { get; set; }
    public string Number { get; set; }
    public long ClientId { get; set; }
    public DateTime Date { get; set; }
    public DocumentStatus Status { get; set; }

    public Client Client { get; set; }
    public ICollection<ShipmentResource> Resources { get; set; }
}

