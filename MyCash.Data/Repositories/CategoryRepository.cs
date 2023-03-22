using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();
    public async ValueTask<Category> InsertAsync(Category category)
    {
        var entity = await cashDbContext.Categories.AddAsync(category);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.Categories.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.Categories.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<Category> SelectAllAsync() =>
        cashDbContext.Categories;

    public async ValueTask<Category> SelectAsync(long id) =>
        await cashDbContext.Categories.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<Category> UpdateAsync(Category category)
    {
        var entity = cashDbContext.Categories.Update(category);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
