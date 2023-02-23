
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;

namespace MyCash.Servise.Services;

public class ExposeService : IExposeService
{
    private readonly GenericRepository<Expose> exposeRepository = new GenericRepository<Expose>();
    private readonly WalletService walletService = new WalletService();

    public async Task<Response<Expose>> CreateAsync(ExposeCreationDto expose)
    {
        var newExpose = new Expose()
        {
            CategoryId = expose.CategoryId,
            Amount = expose.Amount,
            Description = expose.Description,
            WalletId = expose.WalletId,
        };

        var result = await this.exposeRepository.InsertAsync(newExpose);


        var walletResult = await walletService.ChangeAmount(expose.WalletId, -expose.Amount);

        return new Response<Expose>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<bool>> DeleteAsync(long walletId, long id)
    {
        var result = await this.exposeRepository.SelectAsync(x => x.WalletId == walletId && x.Id == id);
        var amount = result.Amount;
        if (result is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await this.exposeRepository.DeleteAsync(x => x.WalletId == walletId && x.Id == id);
        await walletService.ChangeAmount(walletId, amount);

        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async Task<Response<List<Expose>>> GetAllAsync(Predicate<Expose> predicate)
    {
        var results = await this.exposeRepository.SelectAllAsync(predicate);
        return new Response<List<Expose>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = results
        };
    }

    public async Task<Response<Expose>> GetAsync(Predicate<Expose> predicate)
    {
        var result = await this.exposeRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<Expose>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        return new Response<Expose>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<Expose>> UpdateAsync(long walletId, long id, ExposeCreationDto expose)
    {
        var result = await this.exposeRepository.SelectAsync(x => x.Id == id);
        if (result is null)
        {
            return new Response<Expose>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newExpose = new Expose()
        {
            Id = result.Id,
            Description = expose.Description,
            Amount = expose.Amount,
            CategoryId = expose.CategoryId,
            WalletId = expose.WalletId,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.exposeRepository.UpdateAsync(x => x.Id == id, newExpose);

        await walletService.ChangeAmount(walletId, result.Amount);
        await walletService.ChangeAmount(walletId, -newExpose.Amount);

        return new Response<Expose>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }
}
