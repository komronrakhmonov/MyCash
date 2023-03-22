using MyCash.Domain.Entities;

namespace MyCash.Data.IRepositories;

public interface IExchangeRateRepository
{
    ValueTask<ExchangeRateForUSD> InsertAsync(ExchangeRateForUSD exchangeRate);
    ValueTask<ExchangeRateForUSD> UpdateAsync(ExchangeRateForUSD exchangeRate);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ExchangeRateForUSD> SelectAsync(long id);
    IQueryable<ExchangeRateForUSD> SelectAllAsync();
}
