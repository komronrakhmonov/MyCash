
namespace MyCash.Data.IRepositories;

public interface IGenericRepository<TResult>
{
    Task<TResult> InsertAsync(TResult value);
    Task<TResult> UpdateAsync(Predicate<TResult> predicate, TResult value);
    Task<bool> DeleteAsync(Predicate<TResult> predicate);
    Task<TResult> SelectAsync(Predicate<TResult> predicate);
    Task<List<TResult>> SelectAllAsync(Predicate<TResult> predicate = null);
}
