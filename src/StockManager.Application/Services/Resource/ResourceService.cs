using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.Resource;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _repository;

    public ResourceService(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CreateAsync(ResourceCreateDto resourceCreateDto)
    {
        var resourceValidator = new ResourceCreateDtoValidator();
        var result = resourceValidator.Validate(resourceCreateDto);

        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (await _repository.ExistsByNameAsync(resourceCreateDto.Name))
            throw new DuplicateEntryException($"Ресурс с наименованием '{resourceCreateDto.Name}' уже существует.");

        var resource = Mapper.MapToResourceFromResourceCreateDto(resourceCreateDto);

        var resourceId = await _repository.AddAsync(resource);
        return resourceId;
    }

    public async Task ArchiveAsync(long id)
    {
        var ctx = await _repository.GetByIdAsync(id);
        if (ctx == null) throw new EntityNotFoundException($"Ресурс с идентификатором: {id} не найден.");

        if (await _repository.IsUsedAsync(id))
        {
            ctx.Status = Domain.Enums.EntityStatus.Archived;
            await _repository.UpdateResouceAsync(ctx);
            return;
        }

        await _repository.DeleteResourceAsync(ctx.Id);
    }

    public async Task DeleteAsync(long id)
    {
        var exsisting = await _repository.GetByIdAsync(id);
        if (exsisting == null)
            throw new EntityNotFoundException($"Ресурс {id} не найден.");
        if (await _repository.IsUsedAsync(id))
            throw new InvalidStateException("Ресурс используется и не может быть удален; вместо этого заархивируйте его.");
        await _repository.DeleteResourceAsync(id);
    }

    public async Task<ResourceReadDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;
        return new ResourceReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Status = entity.Status.ToString()
        };
    }

    public async Task<IEnumerable<ResourceReadDto>> GetAllAsync(ResourceFilterDto filter)
    {
        var list = await _repository.GetAllAsync();
        var q = list.AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter?.Search))
            q = q.Where(r => r.Name.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));
        if (filter?.OnlyActive == true)
            q = q.Where(r => r.Status.ToString() == "Active");
        return q.Select(e => new ResourceReadDto { Id = e.Id, Name = e.Name, Status = e.Status.ToString() }).ToList();
    }
    public async Task UpdateAsync(ResourceUpdateDto dto)
    {
        var validator = new ResourceCreateDtoValidator();
        var result = validator.Validate(new Dtos.CreateDtos.ResourceCreateDto { Name = dto.Name });
        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        var existing = await _repository.GetByIdAsync(dto.Id) ?? throw new EntityNotFoundException($"Ресурс {dto.Id} не найден.");

        if (!string.Equals(existing.Name, dto.Name, StringComparison.OrdinalIgnoreCase)
            && await _repository.ExistsByNameAsync(dto.Name))
            throw new DuplicateEntryException($"Ресурс с наименованием '{dto.Name}' уже существует.");

        existing.Name = dto.Name;
        await _repository.UpdateResouceAsync(existing);
    }
}
