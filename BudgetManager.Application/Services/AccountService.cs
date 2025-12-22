using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class AccountService(IAccountRepository accountRepository, IUserService userService) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IUserService _userService = userService;

    public async Task CreateAsync(Account account, CancellationToken ct)
        => await _accountRepository.CreateAsync(account, ct);
    public async Task<List<KeyValueDto>?> GetAccountNamesAsync(CancellationToken ct)
    {
        return await _accountRepository.GetAccountNamesAsync(ct) as List<KeyValueDto>;
    }
    public async Task<Account?> GetAccountByIdAsync(int accountId, CancellationToken ct)
        => await _accountRepository.GetAccountByIdAsync(accountId, ct);
    public async Task<List<Account>> GetAccountListAsync(CancellationToken ct)
        => (List<Account>)await _accountRepository.GetAccountsAsync(ct);
    public async Task UpdateAccountAsync(Account account, CancellationToken ct)
        => await _accountRepository.UpdateAccountAsync(account, ct);
    public async Task DeleteAccountAsync(int accountId, CancellationToken ct)
        => await _accountRepository.DeleteAccountAsync(accountId, ct);
}
