using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int userId, CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(int userId, int id, CancellationToken ct);
    Task InsertCategoryAsync(Category category, CancellationToken ct);
    Task UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetCategoryNamesAsync(int userId, CancellationToken ct);
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoByIdAsync(int id, int userId, CancellationToken ct);
    Task DeleteCategoryByIdAsync(int userId, int categoryId, CancellationToken ct);
}
