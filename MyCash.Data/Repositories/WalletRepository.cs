using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();
    public async ValueTask<Wallet> InsertAsync(Wallet wallet)
    {
        var entity = await cashDbContext.Wallets.AddAsync(wallet);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.Wallets.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.Wallets.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<Wallet> SelectAllAsync() =>
        cashDbContext.Wallets;

    public async ValueTask<Wallet> SelectAsync(long id) =>
        await cashDbContext.Wallets.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<Wallet> UpdateAsync(Wallet wallet)
    {
        var entity = cashDbContext.Wallets.Update(wallet);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
