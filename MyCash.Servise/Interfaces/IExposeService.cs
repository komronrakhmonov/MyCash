
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IExposeService
{
    Task<Response<Expose>> CreateAsync(ExposeCreationDto expose);
    Task<Response<bool>> DeleteAsync(long walletId, long id);
    Task<Response<Expose>> GetAsync(Predicate<Expose> predicate);
    Task<Response<List<Expose>>> GetAllAsync(Predicate<Expose> predicate);
    Task<Response<Expose>> UpdateAsync(long walletId, long id, ExposeCreationDto expose);
}
