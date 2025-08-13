using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.ReceiptDocument;

public class ReceiptDocumentService : IReceiptDocumentService
{
    private readonly IReceiptDocumentRepository _receiptRepo;
    private readonly IBalanceRepository _balanceRepo;
    private readonly IResourceRepository _resourceRepo;
    private readonly IMeasurementUnitRepository _muRepo;

    public ReceiptDocumentService(
        IReceiptDocumentRepository receiptRepo,
        IBalanceRepository balanceRepo,
        IResourceRepository resourceRepo,
        IMeasurementUnitRepository muRepo)
    {
        _receiptRepo = receiptRepo;
        _balanceRepo = balanceRepo;
        _resourceRepo = resourceRepo;
        _muRepo = muRepo;
    }

    public async Task<long> CreateAsync(ReceiptDocumentCreateDto dto)
    {
        var validator = new ReceiptDocumentCreateDtoValidator();
        var validation = validator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationFailedException(string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

        if (await _receiptRepo.ExistsByNumberAsync(dto.Number))
            throw new DuplicateEntryException($"Документ поступления с номером '{dto.Number}' уже существует.");

        var entity = Mapper.MapToReceiptDocumentFromReceiptDocumentCreateDto(dto);

        foreach (var item in entity.Resources)
        {
            var res = await _resourceRepo.GetByIdAsync(item.ResourceId)
                ?? throw new EntityNotFoundException("Ресурс не найден");

            var mu = await _muRepo.GetMeasurementUnitByIdAsync(item.MeasurementUnitId)
                ?? throw new EntityNotFoundException("Единица измерения не найдена");

            if (res.Status == Domain.Enums.EntityStatus.Archived)
                throw new InvalidStateException($"Ресурс '{res.Name}' заархивирован и не может быть использован.");

            if (mu.Status == Domain.Enums.EntityStatus.Archived)
                throw new InvalidStateException($"Единица измерения '{mu.Name}' заархивирована и не может быть использована.");

            await _balanceRepo.AdjustAsync(item.ResourceId, item.MeasurementUnitId, item.Quantity);
        }

        return await _receiptRepo.AddAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        var doc = await _receiptRepo.GetByIdWithResourcesAsync(id)
            ?? throw new EntityNotFoundException($"Receipt {id} not found.");

        foreach (var item in doc.Resources)
        {
            var current = await _balanceRepo.GetQuantityAsync(item.ResourceId, item.MeasurementUnitId);
            if (current < item.Quantity)
                throw new InvalidStateException("Недостаточно товара для удаления документа о приёмке. Операция отменена.");

            await _balanceRepo.AdjustAsync(item.ResourceId, item.MeasurementUnitId, -item.Quantity);
        }

        await _receiptRepo.DeleteAsync(doc.Id);
    }

    public async Task<IEnumerable<ReceiptDocumentReadDto>> GetAllAsync(ReceiptFilterDto filter)
    {
        var list = await _receiptRepo.GetAllWithResourcesAsync();
        var q = list.AsQueryable();

        if (filter.From.HasValue) q = q.Where(d => d.Date >= filter.From.Value);
        if (filter.To.HasValue) q = q.Where(d => d.Date <= filter.To.Value);
        if (filter.Numbers != null && filter.Numbers.Any()) q = q.Where(d => filter.Numbers.Contains(d.Number));
        if (filter.ResourceIds != null && filter.ResourceIds.Any())
            q = q.Where(d => d.Resources.Any(r => filter.ResourceIds.Contains(r.ResourceId)));
        if (filter.MeasurementUnitIds != null && filter.MeasurementUnitIds.Any())
            q = q.Where(d => d.Resources.Any(r => filter.MeasurementUnitIds.Contains(r.MeasurementUnitId)));

        return q.Select(entity => new ReceiptDocumentReadDto
        {
            Id = entity.Id,
            Number = entity.Number,
            Date = entity.Date,
            //Status = entity.Status.ToString(),
            Resources = entity.Resources.Select(r => new ReceiptResourceReadDto
            {
                ResourceId = r.ResourceId,
                ResourceName = r.Resource.Name,
                MeasurementUnitId = r.MeasurementUnitId,
                MeasurementUnitName = r.MeasurementUnit.Name,
                Quantity = r.Quantity
            }).ToList()
        }).ToList();
    }

    public async Task<ReceiptDocumentReadDto?> GetByIdAsync(long id)
    {
        var entity = await _receiptRepo.GetByIdWithResourcesAsync(id);
        if (entity == null) return null;
        var dto = new ReceiptDocumentReadDto
        {
            Id = entity.Id,
            Number = entity.Number,
            Date = entity.Date,
            Resources = entity.Resources.Select(r => new ReceiptResourceReadDto
            {
                ResourceId = r.ResourceId,
                ResourceName = r.Resource.Name,
                MeasurementUnitId = r.MeasurementUnitId,
                MeasurementUnitName = r.MeasurementUnit.Name,
                Quantity = r.Quantity
            }).ToList()
        };
        return dto;
    }

    public async Task UpdateAsync(ReceiptDocumentUpdateDto dto)
    {
        var existing = await _receiptRepo.GetByIdWithResourcesAsync(dto.Id)
            ?? throw new EntityNotFoundException($"Ресурс {dto.Id} не найден.");

        var validator = new ReceiptDocumentUpdateDtoValidator();
        var result = validator.Validate(dto);
        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (existing.Number != dto.Number && await _receiptRepo.ExistsByNumberAsync(dto.Number))
            throw new DuplicateEntryException($"Документ поступления с номером '{dto.Number}' уже существует.");

        var oldGrouped = existing.Resources
            .GroupBy(r => (r.ResourceId, r.MeasurementUnitId))
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        var newGrouped = dto.Resources
            .GroupBy(r => (r.ResourceId, r.MeasurementUnitId))
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));

        var allKeys = new HashSet<(long ResourceId, long MeasurementUnitId)>(oldGrouped.Keys);
        allKeys.UnionWith(newGrouped.Keys);

        foreach (var k in newGrouped.Keys)
        {
            var res = await _resourceRepo.GetByIdAsync(k.ResourceId)
                ?? throw new EntityNotFoundException("Ресурс не найден");
            var mu = await _muRepo.GetMeasurementUnitByIdAsync(k.MeasurementUnitId)
                ?? throw new EntityNotFoundException("Единица измерения не найдена");
            if (res.Status == Domain.Enums.EntityStatus.Archived)
                throw new InvalidStateException($"Ресурс '{res.Name}' заархивирован и не может быть использован.");
            if (mu.Status == Domain.Enums.EntityStatus.Archived)
                throw new InvalidStateException($"Единица измерения '{mu.Name}' заархивирована и не может быть использована.");
        }

        foreach (var k in allKeys)
        {
            var oldQty = oldGrouped.TryGetValue(k, out var ov) ? ov : 0m;
            var newQty = newGrouped.TryGetValue(k, out var nv) ? nv : 0m;
            var delta = newQty - oldQty;
            if (delta < 0)
            {
                var current = await _balanceRepo.GetQuantityAsync(k.ResourceId, k.MeasurementUnitId);
                if (current < -delta)
                    throw new InvalidStateException($"Недостаточно запасов для уменьшения единицы {k.MeasurementUnitId} ресурса {k.ResourceId}.");
            }
        }

        foreach (var k in allKeys)
        {
            var oldQty = oldGrouped.TryGetValue(k, out var ov) ? ov : 0m;
            var newQty = newGrouped.TryGetValue(k, out var nv) ? nv : 0m;
            var delta = newQty - oldQty;
            if (delta != 0)
                await _balanceRepo.AdjustAsync(k.ResourceId, k.MeasurementUnitId, delta);
        }

        existing.Number = dto.Number;
        existing.Date = dto.Date;

        existing.Resources.Clear();
        foreach (var item in dto.Resources)
        {
            existing.Resources.Add(new Domain.Entities.ReceiptResource
            {
                ResourceId = item.ResourceId,
                MeasurementUnitId = item.MeasurementUnitId,
                Quantity = item.Quantity
            });
        }

        await _receiptRepo.UpdateReceiptDocumentAsync(existing);
    }
}
