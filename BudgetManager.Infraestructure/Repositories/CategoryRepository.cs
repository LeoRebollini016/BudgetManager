using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class CategoryRepository(IDbConnectionFactory dbConnection) : ICategoryRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int userId)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<CategoryDto>(CategoryQueries.GetCategoriesQuery, new { userId });
    }
    public async Task<Category?> GetCategoryByIdAsync(int userId, int id)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Category>(CategoryQueries.GetCategoryByIdQuery, new { userId, id});
    }
    public async Task<CategoryDeleteDto> GetCategoryDeleteInfoByIdAsync(int id, int userId)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryFirstAsync<CategoryDeleteDto>(CategoryQueries.GetCategoryDeleteInfoQuery, new { id, userId });
    }
    public async Task InsertCategoryAsync(Category category)
    {
        using var conn = _dbConnection.CreateConnection();
        category.Id = await conn.ExecuteAsync(CategoryQueries.InsertCategoryQuery, category);
    }
    public async Task UpdateCategoryAsync(Category category)
    {
        using var conn = _dbConnection.CreateConnection();
        await conn.ExecuteAsync(CategoryQueries.UpdateCategoryQuery, category);
    }
    public async Task<IEnumerable<KeyValueDto>> GetCategoryNamesAsync(int userId)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<KeyValueDto>(CategoryQueries.GetCategoryNamesQuery, new { userId });
    }
    public async Task DeleteCategoryByIdAsync(int userId, int categoryId)
    {
        using var conn = _dbConnection.CreateConnection();
        await conn.ExecuteAsync(CategoryQueries.DisableCategoryByIdQuery, new { userId, id = categoryId });
    }
}
