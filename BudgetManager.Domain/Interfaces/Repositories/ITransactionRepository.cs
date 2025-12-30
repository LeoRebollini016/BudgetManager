using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(Guid userId, CancellationToken ct);
    Task InsertTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct);
    Task UpdateTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct);
    Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(Guid userId, int transactionId, CancellationToken ct);
    Task DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
}
