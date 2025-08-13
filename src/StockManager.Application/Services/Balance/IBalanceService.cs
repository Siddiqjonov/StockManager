using StockManager.Application.Dtos.Filters;
using StockManager.Application.Dtos.GetDtos;

namespace StockManager.Application.Services.Balance;

public interface IBalanceService
{
    Task<IEnumerable<BalanceReadDto>> GetAllAsync(BalanceFilterDto filter);
    Task<BalanceReadDto?> GetByResourceAndUnitAsync(long resourceId, long measurementUnitId);
}