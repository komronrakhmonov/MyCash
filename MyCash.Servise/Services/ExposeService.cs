
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;

namespace MyCash.Servise.Services;

public class ExposeService : IExposeService
{
    private readonly IExposeRepository exposeRepository= new ExposeRepository();
    private readonly IWalletService walletService = new WalletService();
    private readonly IMapper mapper;

    public ExposeService()
    {

    }
    public ExposeService(IMapper mapper)
    {
        this.mapper = mapper;
    }
    public ExposeService(IMapper mapper, IWalletService walletService)
    {
        this.mapper = mapper;
        this.walletService = walletService;
    }

    public async ValueTask<Response<ExposeDto>> CreateAsync(ExposeDto exposeDto)
    {
        var newExpose = mapper.Map<Expose>(exposeDto);
        var expose = await exposeRepository.InsertAsync(newExpose);
        var wallet = await walletService.ChangeAmount(expose.WalletId, -expose.Amount);
        var mappedExpose = mapper.Map<ExposeDto>(newExpose);

        return new Response<ExposeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedExpose
        };

    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var expose = await this.exposeRepository.SelectAsync(id);
        var amount = expose.Amount;
        if (expose is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await exposeRepository.DeleteAsync(id);
        await walletService.ChangeAmount(expose.WalletId, amount);

        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<ExposeDto>>> GetAllAsync()
    {
        var exposes = exposeRepository.SelectAllAsync()
                                      .Include(x => x.Wallet)
                                          .ThenInclude(w => w.User);
        var mappedExposes = mapper.Map<List<ExposeDto>>(exposes);
        return new Response<List<ExposeDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedExposes
        };
    }

    public async ValueTask<Response<ExposeDto>> GetAsync(long id)
    {
        var expose = await exposeRepository.SelectAsync(id);
        if (expose is null)
        {
            return new Response<ExposeDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedExpose = mapper.Map<ExposeDto>(expose);
        return new Response<ExposeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedExpose
        };
    }

    public async ValueTask<Response<ExposeDto>> UpdateAsync(long id, ExposeDto exposeDto)
    {
        var expose = await exposeRepository.SelectAsync(id);
        if (expose is null)
        {
            return new Response<ExposeDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        await walletService.ChangeAmount(expose.WalletId, expose.Amount);
        expose.Description = exposeDto.Description;
        expose.Amount = exposeDto.Amount;
        expose.UpdatedAt = DateTime.Now;
        await walletService.ChangeAmount(expose.WalletId, -expose.Amount);

        var result = await exposeRepository.UpdateAsync(expose);
        var mappedExpose = mapper.Map<ExposeDto>(result);

        return new Response<ExposeDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedExpose
        };
    }

    public async ValueTask<Response<List<ExposeDto>>> GetAllByWalletIdAsync(long walletId)
    {
        var exposes = exposeRepository.SelectAllAsync()
                                      .Where(s => s.WalletId.Equals(walletId))
                                      .Include(x => x.Wallet)
                                          .ThenInclude(w => w.User);
        var mappedExposes = mapper.Map<List<ExposeDto>>(exposes);
        return new Response<List<ExposeDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedExposes
        };
    }
}
