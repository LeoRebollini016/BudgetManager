using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken ct);
    Task AddCategoryAsync(Category category, CancellationToken ct);
    Task UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<List<KeyValueDto>> GetCategoryNamesAsync(CancellationToken ct);
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(int categoryId, CancellationToken ct);
    Task DeleteCategoryByIdAsync(int categoryId, CancellationToken ct);
}
