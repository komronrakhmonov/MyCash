using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IWalletService
{
    ValueTask<Response<WalletDto>> CreateAsync(long userId, WalletDto walletDto);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<WalletDto>> GetAsync(long id);
    ValueTask<Response<List<WalletDto>>> GetAllAsync();
    ValueTask<Response<List<WalletDto>>> GetAllByUserIDAsync(long userId);
    ValueTask<Response<WalletDto>> UpdateAsync(long id, WalletDto walletDto);
    ValueTask<Response<WalletDto>> ChangeAmount(long id, decimal amount);
}
