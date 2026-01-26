using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(Guid userId, CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(Guid userId, int id, CancellationToken ct);
    Task<Result> AddCategoryAsync(Category category, CancellationToken ct);
    Task<Result> UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<List<KeyValueDto>> GetCategoryNamesAsync(Guid userId, CancellationToken ct);
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoAsync(Guid userId, int categoryId, CancellationToken ct);
    Task<Result> DeleteCategoryByIdAsync(Guid userId, int categoryId, CancellationToken ct);
}
