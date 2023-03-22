
using AutoMapper;
using MyCash.Data.IRepositories;
using MyCash.Data.Repositories;
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;
using MyCash.Servise.Interfaces;
using System.Threading.Tasks;

namespace MyCash.Servise.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository = new CategoryRepository();
    private readonly IMapper mapper;
    public CategoryService()
    {

    }
    public CategoryService(IMapper mapper)
    {
        this.mapper = mapper;
    }
    public async ValueTask<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = categoryRepository.SelectAllAsync()
            .FirstOrDefault(x => x.Name.ToLower() == categoryDto.Name.ToLower());
        if (category is null)
        {
            var newCategory = mapper.Map<Category>(categoryDto);   
            var newResult = await categoryRepository.InsertAsync(newCategory);
            var mappedCategory = mapper.Map<CategoryDto>(newResult);

            return new Response<CategoryDto>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = mappedCategory
            };
        }
        return new Response<CategoryDto>()
        {
            StatusCode = 403,
            Message = "Category is alredy exists!",
            Result = null
        };
    }

    public async ValueTask<Response<bool>> DeleteAsync(long id)
    {
        var category = await categoryRepository.SelectAsync(id);
        if (category is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = false
            };
        }
        await categoryRepository.DeleteAsync(id);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = true
        };
    }

    public async ValueTask<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = categoryRepository.SelectAllAsync();
        var mappedCategoris = mapper.Map<List<CategoryDto>>(categories);
        return new Response<List<CategoryDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedCategoris
        };
    }

    public async ValueTask<Response<CategoryDto>> GetAsync(long id)
    {
        var category = await categoryRepository.SelectAsync(id);
        if (category is null)
        {
            return new Response<CategoryDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        var mappedCategory = mapper.Map<CategoryDto>(category); 
        return new Response<CategoryDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedCategory
        };
    }

    public async ValueTask<Response<CategoryDto>> UpdateAsync(long id, CategoryDto categoryDto)
    {
        var category = await categoryRepository.SelectAsync(id);
        if (category is null)
        {
            return new Response<CategoryDto>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Result = null
            };
        }
        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.UpdatedAt = DateTime.Now;
        
        await categoryRepository.UpdateAsync(category);
        var mappedCategory = mapper.Map<CategoryDto>(category); 

        return new Response<CategoryDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Result = mappedCategory
        };
    }
}
