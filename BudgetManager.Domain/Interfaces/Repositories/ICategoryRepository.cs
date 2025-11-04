using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync(int userId);
    Task<Category?> GetCategoryByIdAsync(int userId, int id);
    Task InsertCategoryAsync(Category category);
}
