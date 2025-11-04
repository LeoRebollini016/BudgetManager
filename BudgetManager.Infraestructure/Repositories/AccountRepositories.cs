using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class AccountRepositories(IDbConnectionFactory connectionFactory) : IAccountRepositories
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task CreateAsync(Account account)
    {
        using var conn = _connectionFactory.CreateConnection();
        account.Id = await conn.QuerySingleAsync<int>(AccountQueries.InsertAccountQuery, account);
    }
    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryAsync<Account>(AccountQueries.SelectListAccountsQuery);
    }
    public async Task<Account?> GetAccountByIdAsync(int id)
    {
        using var conn = _connectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Account?>(AccountQueries.SelectAccountByIdQuery, new { id });
    }
    public async Task UpdateAccountAsync(Account account)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountQueries.UpdateAccountQuery, account );
    }
    public async Task DeleteAccountAsync(int id)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(AccountQueries.DeleteAccountQuery, new { id });
    }
}
