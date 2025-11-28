using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Account;

namespace BudgetManager.Domain.Interfaces.Services;

public interface IAccountService
{
    Task CreateAsync(AccountDto accountDto);
    Task<List<KeyValueDto>> GetAccountTypesNamesAsync();
    Task<List<AccountDto>> GetListAccountsAsync();
    Task DeleteAccountAsync(int accountId);
    Task<AccountDto?> GetAccountByIdAsync(int accountId);
    Task UpdateAccountAsync(AccountDto account);
}
