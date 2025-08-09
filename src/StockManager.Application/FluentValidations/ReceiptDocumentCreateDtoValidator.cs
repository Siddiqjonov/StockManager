using FluentValidation;
using StockManager.Application.Dtos.CreateDtos;

namespace StockManager.Application.FluentValidations;

public class ReceiptDocumentCreateDtoValidator : AbstractValidator<ReceiptDocumentCreateDto>
{
    public ReceiptDocumentCreateDtoValidator()
    {
        RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Date).NotEmpty();
    }
}
