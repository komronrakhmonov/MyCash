
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IUserService
{
    Task<Response<User>> CreateAsync(UserCreationDto user);
    Task<Response<bool>> DeleteAsync(Predicate<User> predicate);
    Task<Response<User>> GetAsync(Predicate<User> predicate);
    Task<Response<List<User>>> GetAllAsync(Predicate<User> predicate);
    Task<Response<User>> UpdateAsync(Predicate<User> predicate, UserCreationDto user);
    Task<Response<User>> CheckForExists (string email, string password);
}
