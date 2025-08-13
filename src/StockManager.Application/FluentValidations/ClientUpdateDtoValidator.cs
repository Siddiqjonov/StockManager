using FluentValidation;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.FluentValidations;

public class ClientUpdateDtoValidator : AbstractValidator<ClientUpdateDto>
{
    public ClientUpdateDtoValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0);
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
    }
}
