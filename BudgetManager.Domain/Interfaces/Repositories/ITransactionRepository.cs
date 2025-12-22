using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(int userId, CancellationToken ct);
    Task InsertTransactionAsync(TransactionCreateDto transaction, CancellationToken ct);
    Task UpdateTransactionAsync(TransactionCreateDto transaction, CancellationToken ct);
    Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId, int userId, CancellationToken ct);
    Task DeleteTransactionByIdAsync(int transactionId, int userId, CancellationToken ct);
}
