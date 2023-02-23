
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;
using System;

namespace MyCash.Servise.Services;

public class WalletService : IWalletService
{
    private readonly GenericRepository<Wallet> walletRepository = new GenericRepository<Wallet>();

    public async Task<Response<Wallet>> ChangeAmount(long id, decimal amount)
    {
        var result = await this.walletRepository.SelectAsync(x => x.Id == id);
        if (result is null)
        {
            return new Response<Wallet>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newWallet = new Wallet()
        {
            Id = result.Id,
            Name = result.Name,
            Amount = result.Amount + amount,
            Currency = result.Currency,
            UserId = result.UserId,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.walletRepository.UpdateAsync(x =>x.Id == id, newWallet);

        return new Response<Wallet>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<Wallet>> CreateAsync(long userId, WalletCreationDto wallet)
    {
        var results = await this.walletRepository.SelectAllAsync();
        var result = results.FirstOrDefault(x => x.Name.ToLower() == wallet.Name.ToLower() && wallet.UserId == userId);
        if (result is null)
        {
            var newWallet = new Wallet()
            {
                Name = wallet.Name,
                Currency = wallet.Currency,
                UserId = userId,
            };            

            var newResult = await this.walletRepository.InsertAsync(newWallet);
          
            return new Response<Wallet>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = newResult
            };
        }
        return new Response<Wallet>()
        {
            StatusCode = 403,
            Message = "This wallet is already  exists!",
            Result = null
        };
    }

    public async Task<Response<bool>> DeleteAsync(long userId, long id)
    {
        var result = await this.walletRepository.SelectAsync(x => x.UserId == x.UserId && x.Id == id);
        if (result is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await this.walletRepository.DeleteAsync(x => x.UserId == x.UserId && x.Id == id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async Task<Response<List<Wallet>>> GetAllAsync(Predicate<Wallet> predicate)
    {
        var results = await this.walletRepository.SelectAllAsync(predicate);
        return new Response<List<Wallet>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = results
        };
    }

    public async Task<Response<Wallet>> GetAsync(Predicate<Wallet> predicate)
    {
        var result = await this.walletRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<Wallet>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        return new Response<Wallet>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<Wallet>> UpdateAsync(long Id, WalletCreationDto wallet)
    {
        var result = await this.walletRepository.SelectAsync(x => x.Id == Id);
        if (result is null)
        {
            return new Response<Wallet>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newWallet = new Wallet()
        {
            Id = result.Id,
            Name = wallet.Name,
            Amount = wallet.Amount,
            Currency = wallet.Currency,
            UserId = result.UserId,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.walletRepository.UpdateAsync(x => x.Id == Id, newWallet);

        return new Response<Wallet>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }
}
