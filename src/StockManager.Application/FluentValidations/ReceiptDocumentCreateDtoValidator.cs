using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ReceiptDocumentCreateDtoValidator : AbstractValidator<ReceiptDocumentCreateDto>
{
    public ReceiptDocumentCreateDtoValidator()
    {
        RuleFor(x => x.Number).NotEmpty().WithMessage("Номер документа обязателен.")
                              .MaximumLength(50);

        RuleFor(x => x.Date).NotEmpty().WithMessage("Дата обязательна.");

        RuleForEach(x => x.Resources).ChildRules(r =>
        {
            r.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Количество должно быть > 0.");
            r.RuleFor(x => x.ResourceId).GreaterThan(0);
            r.RuleFor(x => x.MeasurementUnitId).GreaterThan(0);
        });
    }
}
