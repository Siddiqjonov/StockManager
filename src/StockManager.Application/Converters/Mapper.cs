using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.EnumDtos;
using StockManager.Domain.Entities;
using StockManager.Domain.Enums;

namespace StockManager.Application.Converters;

public static class Mapper
{
    public static Resource MapToResourceFromResourceCreateDto(ResourceCreateDto resourceCreateDto)
    {
        return new Resource
        {
            Name = resourceCreateDto.Name,
            Status = (EntityStatus)EntityStatusDto.Active,
        };
    }
    public static MeasurementUnit MapToMeasurementUnitFromMeasurementUnitCreateDto(MeasurementUnitCreateDto dto)
    {
        return new MeasurementUnit
        {
            Name = dto.Name,
            Status = (EntityStatus)EntityStatusDto.Active
        };
    }

    public static Client MapToClientFromClientCreateDto(ClientCreateDto dto)
    {
        return new Client
        {
            Name = dto.Name,
            Address = dto.Address,
            Status = (Domain.Enums.EntityStatus)EntityStatusDto.Active
        };
    }

    public static ReceiptDocument MapToReceiptDocumentFromReceiptDocumentCreateDto(ReceiptDocumentCreateDto dto)
    {
        return new ReceiptDocument
        {
            Number = dto.Number,
            Date = dto.Date
        };
    }

    public static ShipmentDocument MapToShipmentDocumentFromShipmentDocumentCreateDto(ShipmentDocumentCreateDto dto)
    {
        return new ShipmentDocument
        {
            Number = dto.Number,
            ClientId = dto.ClientId,
            Date = dto.Date,
            Status = (DocumentStatus)dto.Status,
            IsSigned = false, // enforce default false on create
            Resources = dto.Resources.Select(r => new ShipmentResource
            {
                ResourceId = r.ResourceId,
                MeasurementUnitId = r.MeasurementUnitId,
                Quantity = r.Quantity
            }).ToList()
        };
    }
}
