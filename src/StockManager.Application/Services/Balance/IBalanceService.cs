namespace StockManager.Application.Services.Balance;

public interface IBalanceService
{
    Task<IEnumerable<BalanceReadDto>> GetAllAsync(BalanceFilterDto filter);
    Task<BalanceReadDto?> GetByResourceAndUnitAsync(long resourceId, long measurementUnitId);
}