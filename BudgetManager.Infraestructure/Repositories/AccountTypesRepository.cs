using BudgetManager.Interfaces.Repositories;
using Dapper;
using BudgetManager.Constants.Queries;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces;

namespace BudgetManager.Services.AccountTypesRepositories;

public class AccountTypesRepository(IDbConnectionFactory connectionFactory) : IAccountTypesRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task CreateAsync(AccountTypes accountTypes, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var id = await conn.QuerySingleAsync<int>(AccountTypesQueries.CreateAccTypesQuery, accountTypes);
        accountTypes.Id = id;
    }
    public async Task<bool> ExistAccTypesAsync(string name, int userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var exist = await conn.QueryFirstOrDefaultAsync<int>(AccountTypesQueries.ExistAccTypesByUserIDQuery, new { name, userId});

        return exist == 1;
    }
    public async Task<IEnumerable<AccountTypesDto>> GetListAccountTypesAsync(int userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<AccountTypesDto>(AccountTypesQueries.GetListAccTypesQuery, new { userId });
    }
    public async Task<AccountTypes> UpdateAsync(AccountTypes accountTypes, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountTypesQueries.UpdateAccTypesQuery, accountTypes);
        return accountTypes;
    }
    public async Task<AccountTypesDto?> GetAccTypesByIdAsync(int id, int userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AccountTypesDto>(AccountTypesQueries.GetAccTypesByIdQuery, new { id, userId });
    }
    public async Task DeleteAccTypeAsync(int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountTypesQueries.DeleteAccTypesByIdQuery, new { id });
    }
    public async Task OrderListAsync(IEnumerable<AccountTypes> accounts, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountTypesQueries.SortAccTypesQuery, accounts);
    }
    public async Task<IEnumerable<KeyValueDto>> GetAccountTypesNamesAsync(int userId, CancellationToken ct) {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<KeyValueDto>(AccountTypesQueries.GetAccountTypesNamesQuery, new { userId } );
    }
}
