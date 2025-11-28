using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(CategoryDto categoryDto);
    Task<List<KeyValueDto>> GetCategoryNamesAsync();
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(int categoryId);
    Task DeleteCategoryByIdAsync(int categoryId);
}
