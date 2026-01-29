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
    public async Task<int?> InsertTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        conn.Open();
        using var sqlTx = conn.BeginTransaction();
        try
        {
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
                transaction: sqlTx,
                cancellationToken: ct
            );
            var newId = await conn.QuerySingleAsync<int?>(command);
            if (newId.HasValue)
            {
                transaction.Id = newId.Value;
                sqlTx.Commit();
                return newId;
            }
            else
            {
                sqlTx.Rollback();
                return null;
            }
        }
        catch
        {
            sqlTx.Rollback();
            throw;
        }
    }
    public async Task<bool> UpdateTransactionAsync(Guid userId, TransactionCreateDto transaction, decimal oldAmount, int oldOperationTypeId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        conn.Open();
        using var sqlTx = conn.BeginTransaction();
        try
        {
            await conn.ExecuteAsync(new CommandDefinition(
                TransactionQueries.UpdateAccountBalanceQuery,
                new
                {
                    UserId = userId,
                    transaction.AccountId,
                    oldAmount,
                    oldOperationTypeId,
                    Multiplier = -1
                },
                transaction: sqlTx,
                cancellationToken: ct
            ));

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
                transaction: sqlTx,
                cancellationToken: ct
            );
            var rows = await conn.ExecuteAsync(command);
            if(rows == 0)
            {
                sqlTx.Rollback();
                return false;
            }
            await conn.ExecuteAsync(new CommandDefinition(
                TransactionQueries.UpdateAccountBalanceQuery,
                new
                {
                    UserId = userId,
                    transaction.AccountId,
                    transaction.Amount,
                    transaction.OperationTypeId,
                    Multiplier = 1
                },
                transaction: sqlTx,
                cancellationToken: ct
            ));

            sqlTx.Commit();
            return true;
        }
        catch
        {
            sqlTx.Rollback();
            throw;
        }
    }
    public async Task<TransactionDto?> GetTransactionById(Guid userId, int transactionId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        var command = new CommandDefinition(
            TransactionQueries.GetTransactionByIdQuery,
            new { Id = transactionId, UserId = userId },
            cancellationToken: ct
        );
        return await conn.QueryFirstOrDefaultAsync<TransactionDto?>(command);
    }
    public async Task<bool> DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        conn.Open();
        using var sqlTx = conn.BeginTransaction();
        try
        {
            var transaction = await conn.QuerySingleOrDefaultAsync<TransactionDto>(new CommandDefinition(
                TransactionQueries.GetTransactionByIdQuery,
                new { Id = transactionId, UserId = userId },
                transaction: sqlTx,
                cancellationToken: ct
            ));

            if (transaction is null)
                return false;

            await conn.ExecuteAsync(new CommandDefinition(
                TransactionQueries.UpdateAccountBalanceQuery,
                new
                {
                    UserId = userId,
                    transaction.AccountId,
                    transaction.Amount,
                    transaction.OperationTypeId,
                    Multiplier = -1
                },
                transaction: sqlTx,
                cancellationToken: ct
            ));

            var command = new CommandDefinition(
                TransactionQueries.DeleteTransactionByIdQuery,
                new { Id = transactionId, UserId = userId },
                transaction: sqlTx,
                cancellationToken: ct
            );

            var rows = await conn.ExecuteAsync(command);

            if(rows > 0)
            {
                sqlTx.Commit();
                return true;
            }
            sqlTx.Rollback();
            return false;
        }
        catch
        {
            sqlTx.Rollback();
            throw;
        }
    }
}
