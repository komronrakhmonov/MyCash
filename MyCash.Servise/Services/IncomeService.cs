
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;

namespace MyCash.Servise.Services;

public class IncomeService : IIncomeService
{

    private readonly GenericRepository<Income> incomeRepository = new GenericRepository<Income>();
    private readonly WalletService walletService = new WalletService();

    public async Task<Response<Income>> CreateAsync(IncomeCreationDto income)
    {
        var newIncome = new Income()
        {
            CategoryId = income.CategoryId,
            Amount = income.Amount,
            Description = income.Description,
            WalletId = income.WalletId,
        };

        var result = await this.incomeRepository.InsertAsync(newIncome);


        var walletResult = await walletService.ChangeAmount(income.WalletId, income.Amount);

        return new Response<Income>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };

    }

    public async Task<Response<bool>> DeleteAsync(long walletId, long id)
    {
        var result = await this.incomeRepository.SelectAsync(x => x.WalletId == walletId && x.Id == id);
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
        await this.incomeRepository.DeleteAsync(x => x.WalletId == walletId && x.Id == id);
        await walletService.ChangeAmount(walletId, -amount);

        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async Task<Response<List<Income>>> GetAllAsync(Predicate<Income> predicate)
    {
        var results = await this.incomeRepository.SelectAllAsync(predicate);
        return new Response<List<Income>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = results
        };
    }

    public async Task<Response<Income>> GetAsync(Predicate<Income> predicate)
    {
        var result = await this.incomeRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<Income>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        return new Response<Income>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<Income>> UpdateAsync(long walletId, long id, IncomeCreationDto income)
    {
        var result = await this.incomeRepository.SelectAsync(x => x.Id == id);
        if (result is null)
        {
            return new Response<Income>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newIncome = new Income()
        {
            Id = result.Id,
            Description = income.Description,
            Amount = income.Amount,
            CategoryId = income.CategoryId,
            WalletId = income.WalletId,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.incomeRepository.UpdateAsync(x => x.Id == id, newIncome);

        await walletService.ChangeAmount(walletId, -result.Amount);
        await walletService.ChangeAmount(walletId, newIncome.Amount);

        return new Response<Income>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }
}
