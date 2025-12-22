using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDetailDto>> GetTransactionListAsync(CancellationToken ct);
    Task InsertTransactionAsync(TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task UpdateTransactionAsync(TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId, CancellationToken ct);
    Task DeleteTransactionByIdAsync(int transactionId, CancellationToken ct);
}
