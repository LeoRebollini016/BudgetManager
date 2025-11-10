using BudgetManager.Domain.Dtos;

namespace BudgetManager.Domain.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDetailDto>> GetTransactionListAsync();
    Task InsertTransactionAsync(TransactionCreateDto transactionCreateDto);
}
