using BudgetManager.Domain.Constants.Queries;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using Dapper;

namespace BudgetManager.Infraestructure.Repositories;

public class TransactionRepository(IDbConnectionFactory dbConnection) : ITransactionRepository
{
    private readonly IDbConnectionFactory _dbConnection = dbConnection;

    public async Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(int userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryAsync<TransactionDetailDto>(TransactionQueries.SelectTransactionListQuery, new { userId });
    }
    public async Task InsertTransactionAsync(TransactionCreateDto transaction, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        transaction.Id = await conn.ExecuteAsync(TransactionQueries.InsertTransactionQuery, transaction);
    }
    public async Task UpdateTransactionAsync(TransactionCreateDto transaction, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        await conn.ExecuteAsync(TransactionQueries.UpdateTransactionQuery, transaction);
    }
    public async Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId, int userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        return await conn.QueryFirstAsync<TransactionDeleteDto>(TransactionQueries.GetTransactionByIdQuery, new { Id = transactionId, userId });
    }
    public async Task DeleteTransactionByIdAsync(int transactionId, int userId, CancellationToken ct)
    {
        using var conn = _dbConnection.CreateConnection();
        await conn.ExecuteAsync(TransactionQueries.DeleteTransactionByIdQuery, new { Id = transactionId, userId });
    }
}
