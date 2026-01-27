using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId, CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(Guid userId, int id, CancellationToken ct);
    Task InsertCategoryAsync(Category category, CancellationToken ct);
    Task UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<IEnumerable<KeyValueDto>> GetCategoryNamesAsync(Guid userId, CancellationToken ct);
    Task<CategoryDeleteDto?> GetCategoryDeleteInfoByIdAsync(Guid userId, int id, CancellationToken ct);
    Task DeleteCategoryByIdAsync(Guid userId, int categoryId, CancellationToken ct);
    Task<bool> ExistsCategoryByNameAsync(Guid userId, string name, CancellationToken ct, int? categoryId = null);
    Task<bool> HasTransactionsAsync(Guid userId, int categoryId, CancellationToken ct);
}
