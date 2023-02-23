
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IWalletService
{
    Task<Response<Wallet>> CreateAsync(long userId, WalletCreationDto wallet);
    Task<Response<bool>> DeleteAsync(long id);
    Task<Response<Wallet>> GetAsync(Predicate<Wallet> predicate);
    Task<Response<List<Wallet>>> GetAllAsync(Predicate<Wallet> predicate);
    Task<Response<Wallet>> UpdateAsync(long id, WalletCreationDto wallet);
    Task<Response<Wallet>> ChangeAmount(long id, decimal amount);
}
