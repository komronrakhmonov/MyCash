
using MyCash.Domain.Entities;


namespace MyCash.Data.IRepositories;

public interface IUserRepository
{
    ValueTask<User> InsertAsync(User user);
    ValueTask<User> UpdateAsync(User user);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<User> SelectAsync(long id);
    IQueryable<User> SelectAllAsync();
   
}
