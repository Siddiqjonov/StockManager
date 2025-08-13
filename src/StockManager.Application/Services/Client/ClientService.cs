using StockManager.Application.Converters;
using StockManager.Application.Dtos.CreateDtos;
using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;
using StockManager.Application.Dtos.UpdateDtos;
using StockManager.Application.Errors;
using StockManager.Application.FluentValidations;
using StockManager.Application.Interfaces;

namespace StockManager.Application.Services.Client
{
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

        public async Task<ClientReadDto?> GetByIdAsync(long id)
        {
            //var client = await _repository.GetClientByIdAsync(id);
            //if (client == null)
            //    throw new EntityNotFoundException($"Client {id} not found.");

            //return Mapper.MapToClientReadDto(client);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientReadDto>> GetAllAsync(ClientFilterDto filter)
        {
            //var clients = await _repository.GetAllClientsAsync(filter);
            //return clients.Select(Mapper.MapToClientReadDto);
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(long id, ClientUpdateDto dto)
        {
            //var validator = new ClientUpdateDtoValidator();
            //var result = validator.Validate(dto);

            //if (!result.IsValid)
            //    throw new ValidationFailedException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));

            //var client = await _repository.GetClientByIdAsync(id);
            //if (client == null)
            //    throw new EntityNotFoundException($"Client {id} not found.");

            //if (!string.Equals(client.Name, dto.Name, StringComparison.OrdinalIgnoreCase)
            //    && await _repository.ExistsByNameAsync(dto.Name))
            //{
            //    throw new DuplicateEntryException($"Клиент с наименованием '{dto.Name}' уже существует.");
            //}

            //Mapper.MapToClientFromClientUpdateDto(dto, client);
            //await _repository.UpdateClientAsync(client);

            throw new NotImplementedException();
        }

        public async Task DeleteAsync(long id)
        {
            var client = await _repository.GetClientByIdAsync(id);
            if (client == null)
                throw new EntityNotFoundException($"Client {id} not found.");

            if (await _repository.IsUsedAsync(id))
                throw new InvalidOperationException($"Cannot delete Client {id} because it is in use.");

            await _repository.DeleteClientAsync(id);
        }
    }
}
