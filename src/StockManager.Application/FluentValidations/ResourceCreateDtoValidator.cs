using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ResourceCreateDtoValidator : AbstractValidator<ResourceCreateDto>
{
    public ResourceCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    }
}
