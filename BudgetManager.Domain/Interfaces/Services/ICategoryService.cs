using BudgetManager.Domain.Dtos;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(CategoryDto categoryDto);
    Task<List<KeyValueDto>> GetCategoryNamesAsync();
}
