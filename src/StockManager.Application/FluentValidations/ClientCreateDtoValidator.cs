using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
{
    public ClientCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Название клиента обязательно.")
                            .MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().WithMessage("Адрес обязателен.")
                               .MaximumLength(300);
    }
}
