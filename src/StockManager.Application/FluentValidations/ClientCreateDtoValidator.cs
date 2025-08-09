using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
{
    public ClientCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
    }
}
