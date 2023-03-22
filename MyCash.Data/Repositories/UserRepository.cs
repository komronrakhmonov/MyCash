using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();
    public async ValueTask<User> InsertAsync(User user)
    {
        var entity = await cashDbContext.Users.AddAsync(user);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.Users.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<User> SelectAllAsync() =>
        cashDbContext.Users;

    public async ValueTask<User> SelectAsync(long id) =>
        await cashDbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<User> UpdateAsync(User user)
    {
        var entity =  cashDbContext.Users.Update(user);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
