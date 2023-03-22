using MyCash.Domain.Entities;

namespace MyCash.Data.IRepositories;

public interface IWalletRepository
{
    ValueTask<Wallet> InsertAsync(Wallet wallet);
    ValueTask<Wallet> UpdateAsync(Wallet wallet);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Wallet> SelectAsync(long id);
    IQueryable<Wallet> SelectAllAsync();
}
