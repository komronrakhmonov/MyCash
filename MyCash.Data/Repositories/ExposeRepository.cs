using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class ExposeRepository : IExposeRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();
    public async ValueTask<Expose> InsertAsync(Expose expose)
    {
        var entity = await cashDbContext.Exposes.AddAsync(expose);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.Exposes.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.Exposes.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<Expose> SelectAllAsync() =>
        cashDbContext.Exposes;

    public async ValueTask<Expose> SelectAsync(long id) =>
        await cashDbContext.Exposes.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<Expose> UpdateAsync(Expose expose)
    {
        var entity = cashDbContext.Exposes.Update(expose);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
