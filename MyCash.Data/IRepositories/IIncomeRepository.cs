

using MyCash.Domain.Entities;

namespace MyCash.Data.IRepositories;

public interface IIncomeRepository
{
    ValueTask<Income> InsertAsync(Income income);
    ValueTask<Income> UpdateAsync(Income income);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Income> SelectAsync(long id);
    IQueryable<Income> SelectAllAsync();
}
