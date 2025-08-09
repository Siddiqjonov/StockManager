using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.ShipmentDocument;

public class ShipmentDocumentService : IShipmentDocumentService
{
    private readonly IShipmentDocumentRepository _repository;

    public ShipmentDocumentService(IShipmentDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CreateAsync(ShipmentDocumentCreateDto shipmentDocumentCreateDto)
    {
        var validator = new ShipmentDocumentCreateDtoValidator();
        var result = validator.Validate(shipmentDocumentCreateDto);

        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (await _repository.ExistsByNumberAsync(shipmentDocumentCreateDto.Number))
            throw new DuplicateEntryException($"Документ отгрузки с номером '{shipmentDocumentCreateDto.Number}' уже существует.");

        var shipmentDocument = Mapper.MapToShipmentDocumentFromShipmentDocumentCreateDto(shipmentDocumentCreateDto);
        return await _repository.AddAsync(shipmentDocument);
    } 
}
