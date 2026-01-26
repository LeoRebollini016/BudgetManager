using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;

namespace BudgetManager.Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task<Result> CreateAsync(Account account, CancellationToken ct)
    {
        var isDuplicate = await _accountRepository.ExistsByNameAsync(account.UserId, account.Name, ct);
        if(isDuplicate)
        {
            return Result.Fail($"Ya existe una cuenta activa con el nombre '{account.Name}'.", nameof(account.Name));
        }
        await _accountRepository.CreateAsync(account, ct);
        return Result.Ok();
    }
    public async Task<List<KeyValueDto>?> GetAccountNamesAsync(Guid userId, CancellationToken ct)
        => await _accountRepository.GetAccountNamesAsync(userId, ct) as List<KeyValueDto>;
    public async Task<Account?> GetAccountByIdAsync(Guid userId, int accountId, CancellationToken ct)
        => await _accountRepository.GetAccountByIdAsync(userId, accountId, ct);
    public async Task<List<AccountDto>> GetAccountListAsync(Guid userId, CancellationToken ct)
        => (List<AccountDto>)await _accountRepository.GetAccountsAsync(userId, ct);
    public async Task<Result> UpdateAccountAsync(Account account, CancellationToken ct)
    {
        var existingAccount = await _accountRepository.GetAccountByIdAsync(account.UserId, account.Id, ct);
        if (existingAccount is null)
        {
            return Result.Fail("La cuenta que intenta actualizar no existe.", null);
        }
        var isDuplicate = await _accountRepository.ExistsByNameAsync(account.UserId, account.Name, ct, account.Id);
        if (isDuplicate)
        {
            return Result.Fail($"Ya existe una cuenta activa con ese nombre '{account.Name}'.", nameof(account.Name));
        }
        await _accountRepository.UpdateAccountAsync(account, ct);
        return Result.Ok();
    }
    public async Task<Result> DeleteAccountAsync(Guid userId, int accountId, CancellationToken ct)
    {
        var account = await _accountRepository.GetAccountByIdAsync(userId,  accountId, ct);
        if (account is null)
        {
            return Result.Fail("La cuenta no existe o no tienes permisos.", null);
        }

        await _accountRepository.DeleteAccountAsync(userId, accountId, ct);
        return Result.Ok();
    }
}
