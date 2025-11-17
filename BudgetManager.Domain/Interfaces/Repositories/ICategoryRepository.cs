using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int userId);
    Task<Category?> GetCategoryByIdAsync(int userId, int id);
    Task InsertCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task<IEnumerable<KeyValueDto>> GetCategoryNamesAsync(int userId);
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoByIdAsync(int id, int userId);
    Task DeleteCategoryByIdAsync(int userId, int categoryId);
}
