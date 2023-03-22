using MyCash.Domain.Entities;

namespace MyCash.Data.IRepositories;

public interface IExposeRepository
{
    ValueTask<Expose> InsertAsync(Expose expose);
    ValueTask<Expose> UpdateAsync(Expose expose);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Expose> SelectAsync(long id);
    IQueryable<Expose> SelectAllAsync();
}
