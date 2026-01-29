using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<TransactionDetailDto>> GetTransactionListAsync(Guid userId, CancellationToken ct)
        => (List<TransactionDetailDto>)await _transactionRepository.GetTransactionsAsync(userId, ct);
    public async Task<Result> InsertTransactionAsync(Guid userId, TransactionCreateDto dto, CancellationToken ct)
    {
        var account = await _accountRepository.GetAccountByIdAsync(userId, dto.AccountId, ct);
        if (account is null)
            return Result.Fail("La cuenta no es válida.", nameof(dto.AccountId));

        var category = await _categoryRepository.GetCategoryByIdAsync(userId, dto.CategoryId, ct);
        if (category is null)
            return Result.Fail("La categoría no es válida.", nameof(dto.CategoryId));

        if (dto.OperationTypeId == 2 && account.Balance < dto.Amount)
            return Result.Fail("Fondos insuficientes en la cuenta.", nameof(dto.Amount));
        try
        {
            var transaction = await _transactionRepository.InsertTransactionAsync(userId, dto, ct);
            if (transaction is null)
                return Result.Fail("No se pudo insertar la transacción.", string.Empty);

            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Error procesando la transacción.", string.Empty);
        }
    }
    public async Task<Result> UpdateTransactionAsync(Guid userId, TransactionCreateDto dto, CancellationToken ct)
    {
        var account = await _accountRepository.GetAccountByIdAsync(userId, dto.AccountId, ct);
        if (account is null)
            return Result.Fail("La cuenta no es válida.", nameof(dto.AccountId));

        var category = await _categoryRepository.GetCategoryByIdAsync(userId, dto.CategoryId, ct);
        if (category is null)
            return Result.Fail("La categoría no es válida.", nameof(dto.CategoryId));

        var oldTransaction = await _transactionRepository.GetTransactionById(userId, dto.Id, ct);
        if (oldTransaction is null)
            return Result.Fail("La transacción no existe o no tienes permisos.", string.Empty);

        decimal balanceAdjustment = account.Balance;
        if(oldTransaction.OperationTypeId == 2) balanceAdjustment += oldTransaction.Amount;
        if(oldTransaction.OperationTypeId == 1) balanceAdjustment -= oldTransaction.Amount;

        if (dto.OperationTypeId == 2 && balanceAdjustment < dto.Amount)
            return Result.Fail("Fondos insuficientes en la cuenta.", nameof(dto.Amount));

        try
        {
            var transaction = await _transactionRepository.UpdateTransactionAsync(userId, dto, oldTransaction.Amount, oldTransaction.OperationTypeId, ct);
            if (!transaction)
                return Result.Fail("No se pudo actualizar la transacción.", string.Empty);

            return Result.Ok();
        }
        catch
        {
            return Result.Fail("Error procesando la transacción.", string.Empty);
        }
    }
    public async Task<TransactionDto?> GetTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
        => await _transactionRepository.GetTransactionById(userId, transactionId, ct);
    public async Task<Result> DeleteTransactionByIdAsync(Guid userId, int transactionId, CancellationToken ct)
    {
        var transaction = await _transactionRepository.GetTransactionById(userId, transactionId, ct);
        if (transaction is null)
        {
            return Result.Fail("La transacción no existe o no tienes permisos.", null);
        }
        var result = await _transactionRepository.DeleteTransactionByIdAsync(userId, transactionId, ct);
        if (!result)
        {
            return Result.Fail("No se pudo eliminar la transacción.", string.Empty);
        }
        return Result.Ok();
    }
}
