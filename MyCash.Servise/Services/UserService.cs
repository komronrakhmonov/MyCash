
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;
using System;

namespace MyCash.Servise.Services;

public class UserService : IUserService
{
    private readonly GenericRepository<User> userRepository = new GenericRepository<User>();


    public async Task<Response<User>> CreateAsync(UserCreationDto user)
    {
        var results = await this.userRepository.SelectAllAsync();       
        var result = results.FirstOrDefault(x => x.Email == user.Email);
        if (result is null)
        {
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };

            var newResult = await this.userRepository.InsertAsync(newUser);

            return new Response<User>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = newResult
            };
        }
        return new Response<User>()
        {
            StatusCode = 403,
            Message = "User is alredy exists!",
            Result = null
        };

    }

    public async Task<Response<bool>> DeleteAsync(Predicate<User> predicate)
    {
        var result = await this.userRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await this.userRepository.DeleteAsync(predicate);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async Task<Response<List<User>>> GetAllAsync(Predicate<User> predicate)
    {        
        var results = await this.userRepository.SelectAllAsync(predicate);
        return new Response<List<User>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = results
        };
    }

    public async Task<Response<User>> GetAsync(Predicate<User> predicate)
    {
        var result = await this.userRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<User>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        return new Response<User>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<User>> UpdateAsync(Predicate<User> predicate, UserCreationDto user)
    {
        var result = await this.userRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<User>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newUser = new User()
        {
            Id = result.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.userRepository.UpdateAsync(predicate, newUser);

        return new Response<User>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };

    }
}
