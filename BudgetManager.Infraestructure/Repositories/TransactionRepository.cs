using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class TransactionRepository(IDbConnectionFactory dbConnection) : ITransactionRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;

    public async Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(Guid userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.SelectTransactionListQuery,
            new { UserId = userId },
            cancellationToken: ct
        );
        return await conn.QueryAsync<TransactionDetailDto>(command);
    }
    public async Task InsertTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.InsertTransactionQuery,
            new
            {
                UserId = userId,
                transaction.TransactionDate,
                transaction.Amount,
                transaction.OperationTypeId,
                transaction.Note,
                transaction.AccountId,
                transaction.CategoryId
            },
            cancellationToken: ct
        );
        transaction.Id = await conn.QuerySingleAsync<int>(command);
    }
    public async Task UpdateTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.UpdateTransactionQuery,
            new
            {
                UserId = userId,
                transaction.Id,
                transaction.TransactionDate,
                transaction.Amount,
                transaction.OperationTypeId,
                transaction.Note,
                transaction.AccountId,
                transaction.CategoryId
            },
            cancellationToken: ct
        );
        var rows = await conn.ExecuteAsync(command);

        if (rows == 0)
            throw new InvalidOperationException("No se encontro la transacción o no se permitio la actualización.");
    }
    public async Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(Guid userId, int transactionId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.GetTransactionByIdQuery,
            new { Id = transactionId, UserId = userId },
            cancellationToken: ct
        );
        return await conn.QueryFirstAsync<TransactionDeleteDto>(command);
    }
    public async Task DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.DeleteTransactionByIdQuery,
            new { Id = transactionId, UserId = userId },
            cancellationToken: ct
        );
        var rows = await conn.ExecuteAsync(command);

        if (rows == 0)
            throw new InvalidOperationException("No se encontro la transacción o no se permitio la eliminación.");
    }
}
