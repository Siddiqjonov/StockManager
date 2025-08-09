using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class MeasurementUnitCreateDtoValidator : AbstractValidator<MeasurementUnitCreateDto>
{
    public MeasurementUnitCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    }
}
