using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class CategoryRepository(IDbConnectionFactory dbConnection) : ICategoryRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;

    public async Task<IEnumerable<Category>> GetCategoriesAsync(int userId)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<Category>(CategoryQueries.GetCategoriesQuery, new { userId });
    }
    public async Task<Category?> GetCategoryByIdAsync(int userId, int id)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync(CategoryQueries.GetCategoryByIdQuery, new { userId, id});
    }
    public async Task InsertCategoryAsync(Category category)
    {
        using var conn = _dbConnection.CreateConnection();
        category.Id = await conn.ExecuteAsync(CategoryQueries.InsertCategoryQuery, category);
    }
}
