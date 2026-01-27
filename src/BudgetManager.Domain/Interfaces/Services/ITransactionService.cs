using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDetailDto>> GetTransactionListAsync(Guid userId, CancellationToken ct);
    Task<Result> InsertTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task<Result> UpdateTransactionAsync(Guid userId, TransactionCreateDto transactionCreateDto, CancellationToken ct);
    Task<TransactionDto> GetTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
    Task<Result> DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct);
}
