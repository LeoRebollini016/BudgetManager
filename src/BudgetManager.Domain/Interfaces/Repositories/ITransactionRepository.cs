using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionDetailDto>> GetTransactionsAsync(Guid userId, CancellationToken ct);
    Task<int?> InsertTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct);
    Task<bool> UpdateTransactionAsync(Guid userId, TransactionCreateDto transaction, CancellationToken ct);
    Task<TransactionDto> GetTransactionById(Guid userId, int transactionId, CancellationToken ct);
    Task<bool> DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
}
