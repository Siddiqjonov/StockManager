using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.Measurement;

public class MeasurementUnitService : IMeasurementUnitService
{
    private readonly IMeasurementUnitRepository _repository;

    public MeasurementUnitService(IMeasurementUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CreateAsync(MeasurementUnitCreateDto measurementUnitCreateDto)
    {
        var validator = new MeasurementUnitCreateDtoValidator();
        var result = validator.Validate(measurementUnitCreateDto);

        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (await _repository.ExistsByNameAsync(measurementUnitCreateDto.Name))
            throw new DuplicateEntryException($"Единица измерения с наименованием '{measurementUnitCreateDto.Name}' уже существует.");

        var measurementUnit = Mapper.MapToMeasurementUnitFromMeasurementUnitCreateDto(measurementUnitCreateDto);
        return await _repository.AddAsync(measurementUnit);
    }
}
