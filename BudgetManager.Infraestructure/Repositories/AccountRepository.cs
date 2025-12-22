using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class AccountRepository(IDbConnectionFactory connectionFactory) : IAccountRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task CreateAsync(Account account, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        account.Id = await conn.QuerySingleAsync<int>(AccountQueries.InsertAccountQuery, account);
    }
    public async Task<IEnumerable<Account>> GetAccountsAsync(CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Account>(AccountQueries.GetListAccountsQuery);
    }
    public async Task<Account?> GetAccountByIdAsync(int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Account?>(AccountQueries.GetAccountByIdQuery, new { id });
    }
    public async Task UpdateAccountAsync(Account account, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountQueries.UpdateAccountQuery, account );
    }
    public async Task DeleteAccountAsync(int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountQueries.ClosedAccountQuery, new { id });
    }
    public async Task<IEnumerable<KeyValueDto>> GetAccountNamesAsync(CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<KeyValueDto>(AccountQueries.GetAccountNamesQuery);
    }
}