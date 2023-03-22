

using MyCash.Servise.DTOs;
using MyCash.Servise.Helpers;

namespace MyCash.Servise.Interfaces;

public interface ICategoryService
{
    ValueTask<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    ValueTask<Response<bool>> DeleteAsync(long id);
    ValueTask<Response<CategoryDto>> GetAsync(long id);
    ValueTask<Response<List<CategoryDto>>> GetAllAsync();
    ValueTask<Response<CategoryDto>> UpdateAsync(long id, CategoryDto categoryDto);
}
