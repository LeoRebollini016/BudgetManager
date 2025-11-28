using BudgetManager.Domain.Dtos.Transaction;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDetailDto>> GetTransactionListAsync();
    Task InsertTransactionAsync(TransactionCreateDto transactionCreateDto);
    Task UpdateTransactionAsync(TransactionCreateDto transactionCreateDto);
    Task<TransactionDeleteDto> GetTransactionDeleteInfoByIdAsync(int transactionId);
    Task DeleteTransactionByIdAsync(int transactionId);
}
