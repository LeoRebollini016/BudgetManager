using BudgetManager.Interfaces.Repositories;
using Dapper;
using BudgetManager.Constants.Queries;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Constants.Queries;

namespace BudgetManager.Services.AccountTypesRepositories;

public class AccountTypesRepository(IDbConnectionFactory connectionFactory) : IAccountTypesRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    public async Task CreateAsync(AccountTypes accountTypes, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.CreateAccTypesQuery,
            accountTypes,
            cancellationToken: ct
        );
        accountTypes.Id = await conn.QuerySingleAsync<int>(command);
    }
    public async Task<bool> ExistAccTypesAsync(Guid userId, string name, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.ExistAccTypesByUserIDQuery,
            new { name, userId },
            cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<int>(command) == 1;
    }
    public async Task<IEnumerable<AccountTypesDto>> GetListAccountTypesAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.GetListAccTypesQuery,
            new { userId },
            cancellationToken: ct
        );
        return await conn.QueryAsync<AccountTypesDto>(command);
    }
    public async Task<AccountTypes> UpdateAsync(AccountTypes accountTypes, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.UpdateAccTypesQuery,
            accountTypes,
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
        return accountTypes;
    }
    public async Task<AccountTypesDto?> GetAccTypesByIdAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.GetAccTypesByIdQuery,
            new { id, userId },
            cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<AccountTypesDto>(command);
    }
    public async Task DeleteAccTypeAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.DeleteAccTypesByIdQuery,
            new { id, userId },
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task OrderListAsync(IEnumerable<AccountTypes> accounts, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.SortAccTypesQuery,
            accounts,
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task<IEnumerable<KeyValueDto>> GetAccountTypesNamesAsync(Guid userId, CancellationToken ct) 
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountTypesQueries.GetAccountTypesNamesQuery,
            new { userId },
            cancellationToken: ct
        );
        return await conn.QueryAsync<KeyValueDto>(command);
    }
}