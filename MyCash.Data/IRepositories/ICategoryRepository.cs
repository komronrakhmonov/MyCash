using MyCash.Domain.Entities;

namespace MyCash.Data.IRepositories;

public interface ICategoryRepository
{
    ValueTask<Category> InsertAsync(Category category);
    ValueTask<Category> UpdateAsync(Category category);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Category> SelectAsync(long id);
    IQueryable<Category> SelectAllAsync();
}
