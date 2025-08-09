using FluentValidation;
using StockManager.Application.Dtos.UpdateDtos;

namespace StockManager.Application.FluentValidations;

public class ReceiptDocumentUpdateDtoValidator : AbstractValidator<ReceiptDocumentUpdateDto>
{
    public ReceiptDocumentUpdateDtoValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Требуется номер документа.")
            .MaximumLength(50).WithMessage("Номер документа не должен превышать 50 символов.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Дата не может быть в будущем.");

        RuleForEach(x => x.Resources).SetValidator(new ReceiptResourceUpdateDtoValidator());
    }
}
