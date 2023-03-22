
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface IUserService
{
    ValueTask<Response<UserDto>> CreateAsync(UserDtoForCreation userDtoForCreation);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<UserDto>> GetAsync(long id);
    ValueTask<Response<List<UserDto>>> GetAllAsync();
    ValueTask<Response<UserDto>> UpdateAsync(long id, UserDtoForCreation userDtoForCreation);
    ValueTask<Response<UserDto>> CheckForExists (string email, string password);
}
