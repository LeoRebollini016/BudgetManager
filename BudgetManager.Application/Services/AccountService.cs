using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task CreateAsync(Account account, CancellationToken ct)
        => await _accountRepository.CreateAsync(account, ct);
    public async Task<List<KeyValueDto>?> GetAccountNamesAsync(Guid userId, CancellationToken ct)
        => await _accountRepository.GetAccountNamesAsync(userId, ct) as List<KeyValueDto>;
    public async Task<Account?> GetAccountByIdAsync(Guid userId, int accountId, CancellationToken ct)
        => await _accountRepository.GetAccountByIdAsync(userId, accountId, ct);
    public async Task<List<Account>> GetAccountListAsync(Guid userId, CancellationToken ct)
        => (List<Account>)await _accountRepository.GetAccountsAsync(userId, ct);
    public async Task UpdateAccountAsync(Account account, CancellationToken ct)
        => await _accountRepository.UpdateAccountAsync(account, ct);
    public async Task DeleteAccountAsync(Guid userId, int accountId, CancellationToken ct)
        => await _accountRepository.DeleteAccountAsync(userId, accountId, ct);
}
