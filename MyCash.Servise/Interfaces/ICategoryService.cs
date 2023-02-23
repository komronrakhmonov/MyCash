
using MyCash.Domain.Entities;
using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface ICategoryService
{
    Task<Response<Category>> CreateAsync(CategoryCreationDto category);
    Task<Response<bool>> DeleteAsync(Predicate<Category> predicate);
    Task<Response<Category>> GetAsync(Predicate<Category> predicate);
    Task<Response<List<Category>>> GetAllAsync(Predicate<Category> predicate);
    Task<Response<Category>> UpdateAsync(Predicate<Category> predicate, CategoryCreationDto category);
}
