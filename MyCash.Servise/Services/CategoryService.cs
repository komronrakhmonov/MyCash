
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;

namespace MyCash.Servise.Services;

public class CategoryService : ICategoryService
{
    private readonly GenericRepository<Category> categoryRepository = new GenericRepository<Category>();
    public async Task<Response<Category>> CreateAsync(CategoryCreationDto category)
    {
        var results = await this.categoryRepository.SelectAllAsync();
        var result = results.FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());
        if (result is null)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                Type = category.Type

            };

            var newResult = await this.categoryRepository.InsertAsync(newCategory);

            return new Response<Category>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = newResult
            };
        }
        return new Response<Category>()
        {
            StatusCode = 403,
            Message = "Category is alredy exists!",
            Result = null
        };
    }

    public async Task<Response<bool>> DeleteAsync(Predicate<Category> predicate)
    {
        var result = await this.categoryRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await this.categoryRepository.DeleteAsync(predicate);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async Task<Response<List<Category>>> GetAllAsync(Predicate<Category> predicate)
    {
        var results = await this.categoryRepository.SelectAllAsync(predicate);
        return new Response<List<Category>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = results
        };
    }

    public async Task<Response<Category>> GetAsync(Predicate<Category> predicate)
    {
        var result = await this.categoryRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<Category>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        return new Response<Category>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }

    public async Task<Response<Category>> UpdateAsync(Predicate<Category> predicate, CategoryCreationDto category)
    {
        var result = await this.categoryRepository.SelectAsync(predicate);
        if (result is null)
        {
            return new Response<Category>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }

        var newCategory = new Category()
        {
            Id = result.Id,
            Name = category.Name,
            Description = category.Description,
            Type = category.Type,
            CreatedAt = result.CreatedAt,
            UpdatedAt = DateTime.Now,
        };

        await this.categoryRepository.UpdateAsync(predicate, newCategory);

        return new Response<Category>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = result
        };
    }
}
