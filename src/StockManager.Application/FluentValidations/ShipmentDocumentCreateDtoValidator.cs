using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ShipmentDocumentCreateDtoValidator : AbstractValidator<ShipmentDocumentCreateDto>
{
    public ShipmentDocumentCreateDtoValidator()
    {
        RuleFor(x => x.Number).NotEmpty().WithMessage("Номер документа обязателен.")
                              .MaximumLength(50);
        RuleFor(x => x.ClientId).GreaterThan(0).WithMessage("Id клиента должен быть больше 0.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Дата обязательна.");

        RuleFor(x => x.Resources).Must(list => list != null && list.Count > 0)
            .WithMessage("Документ отгрузки не может быть пустым.");

        RuleForEach(x => x.Resources).ChildRules(r =>
        {
            r.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Количество должно быть > 0.");
            r.RuleFor(x => x.ResourceId).GreaterThan(0);
            r.RuleFor(x => x.MeasurementUnitId).GreaterThan(0);
        });
    }
}
