using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.ReceiptDocument;

public class ReceiptDocumentService : IReceiptDocumentService
{
    private readonly IReceiptDocumentRepository _repository;

    public ReceiptDocumentService(IReceiptDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CreateAsync(ReceiptDocumentCreateDto receiptDocumentCreateDto)
    {
        var receiptDocumentvalidator = new ReceiptDocumentCreateDtoValidator();
        var result = receiptDocumentvalidator.Validate(receiptDocumentCreateDto);

        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (await _repository.ExistsByNumberAsync(receiptDocumentCreateDto.Number))
            throw new DuplicateEntryException($"Документ поступления с номером '{receiptDocumentCreateDto.Number}' уже существует.");

        var receiptDocument = Mapper.MapToReceiptDocumentFromReceiptDocumentCreateDto(receiptDocumentCreateDto);
        return await _repository.AddAsync(receiptDocument);
    }
}
