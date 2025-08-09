using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ResourceCreateDtoValidator : AbstractValidator<ResourceCreateDto>
{
    public ResourceCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Название ресурса обязательно.")
                            .MaximumLength(200).WithMessage("Название ресурса слишком длинное.");
    }
}
