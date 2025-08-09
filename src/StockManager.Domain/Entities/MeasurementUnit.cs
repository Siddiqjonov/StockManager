using StockManager.Domain.Enums;

namespace StockManager.Domain.Entities;

public class MeasurementUnit
{
    public long Id { get; set; }
    public string Name { get; set; }
    public EntityStatus Status { get; set; }

    public ICollection<Balance> Balances { get; set; }
    public ICollection<ReceiptResource> ReceiptResources { get; set; }
    public ICollection<ShipmentResource> ShipmentResources { get; set; }
}
