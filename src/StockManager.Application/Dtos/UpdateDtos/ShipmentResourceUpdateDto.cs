namespace StockManager.Application.Dtos.UpdateDtos;

public class ShipmentResourceUpdateDto
{
    public long Id { get; set; }
    public long ResourceId { get; set; }
    public decimal Quantity { get; set; }
}
