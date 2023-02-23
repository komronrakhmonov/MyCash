
using MyCash.Data.Configurations;
using MyCash.Data.IRepositories;
using MyCash.Domain.Commons;
using MyCash.Domain.Entities;
using Newtonsoft.Json;

namespace MyCash.Data.Repositories;

public class GenericRepository<TResult> : IGenericRepository<TResult> where TResult : Auditable
{
    private string path;
    private long lastId = 0;

    public GenericRepository()
    {
        if (typeof(TResult) == typeof(User))
            path = DataBasePath.USER_PATH;

        else if (typeof(TResult) == typeof(Wallet))
            path = DataBasePath.WALLET_PATH;

        else if (typeof(TResult) == typeof(Income))
            path = DataBasePath.INCOME_PATH;

        else if (typeof(TResult) == typeof(Expose))
            path = DataBasePath.EXPOSE_PATH;

        else if (typeof(TResult) == typeof(Category))
            path = DataBasePath.CATEGORY_PATH;

    }

    public async Task<TResult> InsertAsync(TResult value)
    {
        
        var models = await SelectAllAsync();
        if (models.Count != 0)
            lastId = models.Max(x => x.Id);
        value.Id = ++lastId;
        value.CreatedAt = DateTime.Now;

        models.Add(value);

        var json = JsonConvert.SerializeObject(models, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);

        return value;
    }

    public async Task<bool> DeleteAsync(Predicate<TResult> predicate)
    {
        var models = await SelectAllAsync();
        var model = models.FirstOrDefault(x => predicate(x));

        if (model is null)
            return false;

        models.Remove(model);
        var json = JsonConvert.SerializeObject(models, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);

        return true;

    }

    public async Task<List<TResult>> SelectAllAsync(Predicate<TResult> predicate = null)
    {
        string source = await File.ReadAllTextAsync(path);
        if (string .IsNullOrEmpty(source))
            source = "[]";

        List<TResult> results = JsonConvert.DeserializeObject<List<TResult>>(source);

        if (predicate is not null)
            results = results.FindAll(predicate);

        return results;

    }

    public async Task<TResult> SelectAsync(Predicate<TResult> predicate)
    {
        var results = await SelectAllAsync();
        return results.FirstOrDefault(x => predicate(x));
    }
    public async Task<TResult> UpdateAsync(Predicate<TResult> predicate, TResult value)
    {
        var models = await SelectAllAsync();
        var model = models.FirstOrDefault(x => predicate(x));

       
        if (model is not  null)
        {
            var index = models.IndexOf(model);
            models.Remove(model);

            value.Id = model.Id;
            value.CreatedAt = model.CreatedAt;
            value.UpdatedAt = DateTime.Now;

            models.Insert(index, value);
            var json = JsonConvert.SerializeObject(models, Formatting.Indented);
            await File.WriteAllTextAsync(path, json);

            return model;
        }

        return model;
    }
}
