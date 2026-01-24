using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDetailDto>> GetTransactionListAsync(Guid userId, CancellationToken ct);
    Task InsertTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task UpdateTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task<TransactionDto> GetTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
    Task DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
}
