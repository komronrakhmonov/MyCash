
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;

namespace MyCash.Servise.Services;

public class IncomeService : IIncomeService
{

    private readonly IIncomeRepository incomeRepository = new IncomeRepository();
    private readonly IWalletService walletService = new WalletService();
    private readonly IMapper mapper;

    public IncomeService()
    {

    }
    public IncomeService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public IncomeService(IMapper mapper, IWalletService walletService)
    {
        this.mapper = mapper;
        this.walletService = walletService;
    }
   
    public async ValueTask<Response<IncomeDto>> CreateAsync(IncomeDto incomeDto)
    {
        var newIncome = mapper.Map<Income>(incomeDto);  
        var income = await incomeRepository.InsertAsync(newIncome);
        var wallet = await walletService.ChangeAmount(income.WalletId, income.Amount);
        var mappedIncome = mapper.Map<IncomeDto>(newIncome);

        return new Response<IncomeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedIncome
        };

    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var income = await this.incomeRepository.SelectAsync(id);
        var amount = income.Amount;
        if (income is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await incomeRepository.DeleteAsync(id);
        await walletService.ChangeAmount(income.WalletId, -amount);

        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<IncomeDto>>> GetAllAsync()
    {
        var incomes = incomeRepository.SelectAllAsync()
                                      .Include(x => x.Wallet)
                                          .ThenInclude(w => w.User);
        var mappedIncomes = mapper.Map<List<IncomeDto>>(incomes);
        return new Response<List<IncomeDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedIncomes
        };
    }

    public async ValueTask<Response<IncomeDto>> GetAsync(long id)
    {
        var income = await incomeRepository.SelectAsync(id);
        if (income is null)
        {
            return new Response<IncomeDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedIncome = mapper.Map<IncomeDto>(income);
        return new Response<IncomeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedIncome
        };
    }

    public async ValueTask<Response<IncomeDto>> UpdateAsync(long id, IncomeDto incomeDto)
    {
        var income = await incomeRepository.SelectAsync(id);
        if (income is null)
        {
            return new Response<IncomeDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        await walletService.ChangeAmount(income.WalletId, -income.Amount);
        income.Description = incomeDto.Description;
        income.Amount = incomeDto.Amount;
        income.UpdatedAt = DateTime.Now;
        await walletService.ChangeAmount(income.WalletId, income.Amount);

        var result = await incomeRepository.UpdateAsync(income);
        var mappedIncome = mapper.Map<IncomeDto>(result);

        return new Response<IncomeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedIncome
        };
    }

    public async ValueTask<Response<List<IncomeDto>>> GetAllByWalletIdAsync(long walletId)
    {
        var incomes = incomeRepository.SelectAllAsync()
                                      .Where(s => s.WalletId.Equals(walletId))
                                      .Include(x => x.Wallet)
                                          .ThenInclude(w => w.User);
        var mappedIncomes = mapper.Map<List<IncomeDto>>(incomes);
        return new Response<List<IncomeDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedIncomes
        };
    }
}
