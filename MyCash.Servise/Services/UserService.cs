
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;


namespace MyCash.Servise.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository = new UserRepository();
    private readonly IMapper mapper;
    public UserService()
    {

    }
    public UserService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async ValueTask<Response<UserDto>> CheckForExists(string email, string password)
    {
        var user = userRepository.SelectAllAsync()
                                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        if (user == null)
        {
            return new Response<UserDto>()
            {
                StatusCode = 404,
                Message = "not found",
                Result = null
            };
        }
        var mappedUser = mapper.Map<UserDto>(user); 
        return new Response<UserDto>()
        {
            StatusCode = 200,
            Message = "Succes",
            Result = mappedUser
        };
    }

    public async ValueTask<Response<UserDto>> CreateAsync(UserDtoForCreation userDtoForCreation)
    {
        var user = userRepository.SelectAllAsync()
                                .FirstOrDefault(x => x.Email.Equals(userDtoForCreation.Email));
       
        if (user is null)
        {
            var newUser = mapper.Map<User>(userDtoForCreation);
            var result = await this.userRepository.InsertAsync(newUser);
            var mappedResult = mapper.Map<UserDto>(result);

            return new Response<UserDto>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = mappedResult
            };
        }
        return new Response<UserDto>()
        {
            StatusCode = 403,
            Message = "User is alredy exists!",
            Result = null
        };

    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var result = await userRepository.SelectAsync(id);
        if (result is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await userRepository.DeleteAsync(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<UserDto>>> GetAllAsync()
    {        
        var users = userRepository.SelectAllAsync();
        var mappedUsers = mapper.Map<List<UserDto>>(users);
        return new Response<List<UserDto>>()
        {
            StatusCode = 200,   
            Message = "Success",
            Result = mappedUsers
        };
    }

    public async ValueTask<Response<UserDto>> GetAsync(long id)
    {
        var user = await userRepository.SelectAsync(id);
        if (user is null)
        {
            return new Response<UserDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedUser = mapper.Map<UserDto>(user);
        return new Response<UserDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedUser
        };
    }

    public async ValueTask<Response<UserDto>> UpdateAsync(long id, UserDtoForCreation userDtoForCreation)
    {
        var user = await userRepository.SelectAsync(id);
        if (user is null)
        {
            return new Response<UserDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        user.FirstName = userDtoForCreation.FirstName;
        user.LastName = userDtoForCreation.LastName;
        user.Email = userDtoForCreation.Email;
        user.Password = userDtoForCreation.Password;
        user.UpdatedAt = DateTime.Now;

        var result = await userRepository.UpdateAsync(user);
        var mappedUser = mapper.Map<UserDto>(result);

        return new Response<UserDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedUser
        };

    }
}
