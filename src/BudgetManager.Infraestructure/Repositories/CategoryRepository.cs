using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class CategoryRepository(IDbConnectionFactory dbConnection) : ICategoryRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.GetCategoriesQuery,
            new { userId },
            cancellationToken: ct
        );
        return await conn.QueryAsync<CategoryDto>(command);
    }
    public async Task<Category?> GetCategoryByIdAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.GetCategoryByIdQuery,
            new { userId, id },
            cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<Category>(command);
    }
    public async Task<CategoryDeleteDto> GetCategoryDeleteInfoByIdAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.GetCategoryDeleteInfoQuery,
            new { userId, id },
            cancellationToken: ct
        );
        return await conn.QueryFirstAsync<CategoryDeleteDto>(command);
    }
    public async Task InsertCategoryAsync(Category category, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.InsertCategoryQuery,
            category,
            cancellationToken: ct
        );
        category.Id = await conn.ExecuteAsync(command);
    }
    public async Task UpdateCategoryAsync(Category category, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.UpdateCategoryQuery,
            category,
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task<IEnumerable<KeyValueDto>> GetCategoryNamesAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.GetCategoryNamesQuery,
            new { userId },
            cancellationToken: ct
        );
        return await conn.QueryAsync<KeyValueDto>(command);
    }
    public async Task DeleteCategoryByIdAsync(Guid userId, int categoryId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.DisableCategoryByIdQuery,
            new { userId, id = categoryId },
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task<bool> ExistsCategoryByNameAsync(Guid userId, string name, CancellationToken ct, int? categoryId = null)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.ExistsCategoryByNameQuery,
            new { userId, name, categoryId },
            cancellationToken: ct
        );
        return await conn.ExecuteScalarAsync<int>(command) == 1;
    }

    public async Task<bool> HasTransactionsAsync(Guid userId, int categoryId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            CategoryQueries.HasTransactionsQuery,
            new { userId, categoryId },
            cancellationToken: ct
        );
        return await conn.ExecuteScalarAsync<int>(command) == 1;
    }
}
