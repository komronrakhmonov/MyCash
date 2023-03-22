using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();

    public async ValueTask<ExchangeRateForUSD> InsertAsync(ExchangeRateForUSD exchangeRateForUSD)
    {
        var entity = await cashDbContext.ExchangeRatesForUSD.AddAsync(exchangeRateForUSD);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.ExchangeRatesForUSD
                                    .FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.ExchangeRatesForUSD.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<ExchangeRateForUSD> SelectAllAsync() =>
            cashDbContext.ExchangeRatesForUSD;

    public async ValueTask<ExchangeRateForUSD> SelectAsync(long id) =>
                await cashDbContext.ExchangeRatesForUSD.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<ExchangeRateForUSD> UpdateAsync(ExchangeRateForUSD exchangeRateForUSD)
    {
        var entity = cashDbContext.ExchangeRatesForUSD.Update(exchangeRateForUSD);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
