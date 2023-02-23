
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IIncomeService
{
    Task<Response<Income>> CreateAsync(IncomeCreationDto income);
    Task<Response<bool>> DeleteAsync(long walletId, long id);
    Task<Response<Income>> GetAsync(Predicate<Income> predicate);
    Task<Response<List<Income>>> GetAllAsync(Predicate<Income> predicate);
    Task<Response<Income>> UpdateAsync(long walletId, long id, IncomeCreationDto income);
    
}
