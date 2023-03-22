
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IExposeService
{
    ValueTask<Response<ExposeDto>> CreateAsync(ExposeDto exposeDto);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<ExposeDto>> GetAsync(long id);
    ValueTask<Response<List<ExposeDto>>> GetAllAsync();
    ValueTask<Response<List<ExposeDto>>> GetAllByWalletIdAsync(long WalletId);
    ValueTask<Response<ExposeDto>> UpdateAsync(long id, ExposeDto exposeDto);
}
