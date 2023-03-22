
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;
using System;

namespace MyCash.Servise.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository walletRepository = new WalletRepository();
    private readonly IMapper mapper;
    public WalletService()
    {

    }
    public WalletService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public WalletService(IMapper mapper, IWalletRepository walletRepository)
    {
        this.mapper = mapper;
        this.walletRepository = walletRepository;
    }


    public async ValueTask<Response<WalletDto>> ChangeAmount(long id, decimal amount)
    {
        var wallet = await walletRepository.SelectAsync(id);
        if (wallet is null)
        {
            return new Response<WalletDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        wallet.Amount += amount;
        wallet.UpdatedAt = DateTime.Now;

        await walletRepository.UpdateAsync(wallet);

        var mappedWallet = mapper.Map<WalletDto>(wallet);


        return new Response<WalletDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedWallet
        };
    }

    public async ValueTask<Response<WalletDto>> CreateAsync(long userId, WalletDto walletDto)
    {
        var wallet = walletRepository.SelectAllAsync()
                                        .FirstOrDefault(x => x.Name.ToLower().Equals(walletDto.Name.ToLower())
                                        && walletDto.UserId.Equals(userId));
        if (wallet is null)
        {
            var newWallet = mapper.Map<Wallet>(walletDto);
            var result = await walletRepository.InsertAsync(newWallet);
            var mappedResult = mapper.Map<WalletDto>(result);

            return new Response<WalletDto>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = mappedResult
            };
        }
        return new Response<WalletDto>()
        {
            StatusCode = 403,
            Message = "This wallet is already  exists!",
            Result = null
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var wallet = await this.walletRepository.SelectAsync(id);
        if (wallet is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await walletRepository.DeleteAsync(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<WalletDto>>> GetAllAsync()
    {
        var wallets = walletRepository.SelectAllAsync()
                                    .Include(x => x.User);
        var mappedWallets = mapper.Map<List<WalletDto>>(wallets);
        return new Response<List<WalletDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedWallets
        };
    }

    public async ValueTask<Response<List<WalletDto>>> GetAllByUserIDAsync(long userId)
    {
        var wallets = walletRepository.SelectAllAsync()
                                    .Where(s => s.UserId.Equals(userId))
                                    .Include(x => x.User);

        var mappedWallets = mapper.Map<List<WalletDto>>(wallets);
        return new Response<List<WalletDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedWallets
        };
    }

    public async ValueTask<Response<WalletDto>> GetAsync(long id)
    {
        var wallet = await walletRepository.SelectAsync(id);
        if (wallet is null)
        {
            return new Response<WalletDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedWallet = mapper.Map<WalletDto>(wallet);
        return new Response<WalletDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedWallet
        };
    }

    public async ValueTask<Response<WalletDto>> UpdateAsync(long id, WalletDto walletDto)
    {
        var wallet = await walletRepository.SelectAsync(id);
        if (wallet is null)
        {
            return new Response<WalletDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        wallet.Name = walletDto.Name;
        wallet.Currency = walletDto.Currency;
        wallet.UpdatedAt = DateTime.Now;
        wallet.Amount = walletDto.Amount;

        await this.walletRepository.UpdateAsync(wallet);

        var mappedWallet = mapper.Map<WalletDto>(wallet);

        return new Response<WalletDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedWallet
        };
    }
}
