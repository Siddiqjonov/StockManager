using FluentValidation;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.FluentValidations;

public class ReceiptResourceUpdateDtoValidator : AbstractValidator<ReceiptResourceUpdateDto>
{
    public ReceiptResourceUpdateDtoValidator()
    {
        RuleFor(x => x.ResourceId)
            .GreaterThan(0).WithMessage("Неверный ResourceId.");

        RuleFor(x => x.MeasurementUnitId)
            .GreaterThan(0).WithMessage("Неверный MeasurementUnitId.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Количество должно быть больше нуля.");
    }
}
