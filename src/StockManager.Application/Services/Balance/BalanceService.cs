using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;

namespace StockManager.Application.Services.Balance;

public class BalanceService : IBalanceService
{
    public Task<IEnumerable<BalanceReadDto>> GetAllAsync(BalanceFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<BalanceReadDto?> GetByResourceAndUnitAsync(long resourceId, long measurementUnitId)
    {
        throw new NotImplementedException();
    }
}
