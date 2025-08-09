using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
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
}
