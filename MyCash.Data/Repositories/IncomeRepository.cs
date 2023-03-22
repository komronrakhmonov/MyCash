using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Domain.Entities;

namespace MyCash.Data.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly MyCashDbContext cashDbContext = new MyCashDbContext();
    public async ValueTask<Income> InsertAsync(Income income)
    {
        var entity = await cashDbContext.Incomes.AddAsync(income);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var entity = await cashDbContext.Incomes.FirstOrDefaultAsync(s => s.Id.Equals(id));
        if (entity is null)
            return false;
        cashDbContext.Incomes.Remove(entity);
        await cashDbContext.SaveChangesAsync();
        return true;
    }

    public IQueryable<Income> SelectAllAsync() =>
        cashDbContext.Incomes;

    public async ValueTask<Income> SelectAsync(long id) =>
        await cashDbContext.Incomes.FirstOrDefaultAsync(s => s.Id.Equals(id));

    public async ValueTask<Income> UpdateAsync(Income income)
    {
        var entity = cashDbContext.Incomes.Update(income);
        await cashDbContext.SaveChangesAsync();
        return entity.Entity;
    }
}
