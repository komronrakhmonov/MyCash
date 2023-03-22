
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IIncomeService
{
    ValueTask<Response<IncomeDto>> CreateAsync(IncomeDto incomeDto);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<IncomeDto>> GetAsync(long id);
    ValueTask<Response<List<IncomeDto>>> GetAllAsync();
    ValueTask<Response<List<IncomeDto>>> GetAllByWalletIdAsync(long walletId);
    ValueTask<Response<IncomeDto>> UpdateAsync(long id, IncomeDto incomeDto);
    
}
