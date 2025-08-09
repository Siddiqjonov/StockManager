using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.Client;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> CreateAsync(ClientCreateDto clientCreateDto)
    {
        var validator = new ClientCreateDtoValidator();
        var result = validator.Validate(clientCreateDto);

        if (!result.IsValid)
            throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

        if (await _repository.ExistsByNameAsync(clientCreateDto.Name))
            throw new DuplicateEntryException($"Клиент с наименованием '{clientCreateDto.Name}' уже существует.");

        var client = Mapper.MapToClientFromClientCreateDto(clientCreateDto);
        return await _repository.AddAsync(client);
    }

    public async Task ArchiveAsync(long id)
    {
        var ctx = await _repository.GetClientByIdAsync(id);
        if (ctx == null) throw new EntityNotFoundException($"Client {id} not found.");

        if (await _repository.IsUsedAsync(id))
        {
            ctx.Status = Domain.Enums.EntityStatus.Archived;
            await _repository.UpdateClientAsync(ctx);
            return;
        }

        await _repository.DeleteClientAsync(ctx.Id);
    }
}
