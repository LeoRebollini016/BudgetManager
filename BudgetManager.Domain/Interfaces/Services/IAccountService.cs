using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountService
{
    Task<Result> CreateAsync(Account account, CancellationToken cancellationToken);
    Task<List<AccountDto>> GetAccountListAsync(Guid userId, CancellationToken cancellationToken);
    Task<Result> DeleteAccountAsync(Guid userId, int accountId, CancellationToken cancellationToken);
    Task<Account?> GetAccountByIdAsync(Guid userId, int accountId, CancellationToken cancellationToken);
    Task<Result> UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task<List<KeyValueDto>?> GetAccountNamesAsync(Guid userId, CancellationToken cancellationToken);
}
