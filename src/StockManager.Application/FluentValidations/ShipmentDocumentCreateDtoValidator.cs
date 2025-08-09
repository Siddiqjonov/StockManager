using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ShipmentDocumentCreateDtoValidator : AbstractValidator<ShipmentDocumentCreateDto>
{
    public ShipmentDocumentCreateDtoValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ClientId).GreaterThan(0);
        RuleFor(x => x.Date).NotEmpty();
    }
}
