using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.ShipmentDocument;

public class ShipmentDocumentService : IShipmentDocumentService
{
    private readonly IShipmentDocumentRepository _shipmentRepo;
    private readonly IBalanceRepository _balanceRepo;
    private readonly IResourceRepository _resourceRepo;
    private readonly IMeasurementUnitRepository _muRepo;
    private readonly IClientRepository _clientRepo;

    public ShipmentDocumentService(
        IShipmentDocumentRepository shipmentRepo,
        IBalanceRepository balanceRepo,
        IResourceRepository resourceRepo,
        IMeasurementUnitRepository muRepo,
        IClientRepository clientRepo)
    {
        _shipmentRepo = shipmentRepo;
        _balanceRepo = balanceRepo;
        _resourceRepo = resourceRepo;
        _muRepo = muRepo;
        _clientRepo = clientRepo;
    }

    public async Task<long> CreateAsync(ShipmentDocumentCreateDto dto)
    {
        var validator = new ShipmentDocumentCreateDtoValidator();
        var res = validator.Validate(dto);
        if (!res.IsValid)
            throw new ValidationFailedException(string.Join("; ", res.Errors.Select(e => e.ErrorMessage)));

        if (await _shipmentRepo.ExistsByNumberAsync(dto.Number))
            throw new DuplicateEntryException($"Документ отгрузки с номером '{dto.Number}' уже существует.");

        if (await _clientRepo.ExistsByIdAsync(dto.ClientId) == null)
            throw new EntityNotFoundException($"Клиент {dto.ClientId} не найден.");

        foreach (var item in dto.Resources)
        {
            var r = await _resourceRepo.GetByIdAsync(item.ResourceId) ?? throw new EntityNotFoundException("Ресурс не найден");
            var mu = await _muRepo.GetMeasurementUnitByIdAsync(item.MeasurementUnitId) ?? throw new EntityNotFoundException("Единица измерения не найдена");
            if (r.Status == Domain.Enums.EntityStatus.Archived) throw new InvalidStateException($"Ресурс '{r.Name}' заархивирован.");
            if (mu.Status == Domain.Enums.EntityStatus.Archived) throw new InvalidStateException($"Единица измерения архивирована.");
        }

        var entity = Mapper.MapToShipmentDocumentFromShipmentDocumentCreateDto(dto);
        return await _shipmentRepo.AddAsync(entity);
    }

    public async Task SignAsync(long id)
    {
        var doc = await _shipmentRepo.GetShipmentDocumentByIdWithResourcesAsync(id) ?? throw new EntityNotFoundException($"Shipment {id} not found.");
        if (doc.IsSigned) throw new InvalidStateException("Document already signed.");

        // Check if enough stock for all resources (sum by resource+unit)
        var grouped = doc.Resources.GroupBy(r => (r.ResourceId, r.MeasurementUnitId))
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        foreach (var kv in grouped)
        {
            var current = await _balanceRepo.GetQuantityAsync(kv.Key.ResourceId, kv.Key.MeasurementUnitId);
            if (current < kv.Value)
                throw new InvalidStateException($"Not enough stock for resource {kv.Key.ResourceId} unit {kv.Key.MeasurementUnitId}.");
        }

        // apply decreases
        foreach (var kv in grouped)
        {
            await _balanceRepo.AdjustAsync(kv.Key.ResourceId, kv.Key.MeasurementUnitId, -kv.Value);
        }

        doc.IsSigned = true;
        doc.Status = Domain.Enums.DocumentStatus.Signed;
        await _shipmentRepo.UpdateAsync(doc);
    }

    public async Task RevokeAsync(long id)
    {
        var doc = await _shipmentRepo.GetShipmentDocumentByIdWithResourcesAsync(id) ?? throw new EntityNotFoundException($"Shipment {id} not found.");
        if (!doc.IsSigned) throw new InvalidStateException("Document is not signed.");

        // increase balances back
        var grouped = doc.Resources.GroupBy(r => (r.ResourceId, r.MeasurementUnitId))
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        foreach (var kv in grouped)
        {
            await _balanceRepo.AdjustAsync(kv.Key.ResourceId, kv.Key.MeasurementUnitId, kv.Value);
        }

        doc.IsSigned = false;
        doc.Status = Domain.Enums.DocumentStatus.Revoked;
        await _shipmentRepo.UpdateAsync(doc);
    }
}
