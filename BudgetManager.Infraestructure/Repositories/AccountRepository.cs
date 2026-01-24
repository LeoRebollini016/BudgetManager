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
        var command = new CommandDefinition(
            AccountQueries.InsertAccountQuery,
            account,
            cancellationToken: ct
        );
        account.Id = await conn.QuerySingleAsync<int>(command);
    }
    public async Task<IEnumerable<Account>> GetAccountsAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
           AccountQueries.GetListAccountsQuery,
           new { UserId = userId },
           cancellationToken: ct
        );
        return await conn.QueryAsync<Account>(command);
    }
    public async Task<Account?> GetAccountByIdAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
           AccountQueries.GetAccountByIdQuery,
           new { Id = id, UserId = userId },
           cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<Account?>(command);
    }
    public async Task UpdateAccountAsync(Account account, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountQueries.UpdateAccountQuery,
            account,
            cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task DeleteAccountAsync(Guid userId, int id, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
           AccountQueries.ClosedAccountQuery,
           new { Id = id, UserId = userId },
           cancellationToken: ct
        );
        await conn.ExecuteAsync(command);
    }
    public async Task<IEnumerable<KeyValueDto>> GetAccountNamesAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
           AccountQueries.GetAccountNamesQuery,
           new { UserId = userId },
           cancellationToken: ct
        );
        return await conn.QueryAsync<KeyValueDto>(command);
    }

    public async Task<bool> ExistsByNameAsync(Guid userId, string name, CancellationToken ct)
    {
        using var conn = _connectionFactory.CreateConnection();
        var command = new CommandDefinition(
            AccountQueries.ExistsByNameQuery,
            new { UserId = userId, Name = name },
            cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<int?>(command) == 1;
    }
}