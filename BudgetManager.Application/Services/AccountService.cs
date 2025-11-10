using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;

namespace BudgetManager.Application.Services;

public class AccountService(IAccountRepository accountRepository, IUserService userService, IAccountTypesRepository accountTypesService, IMapper mapper) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IUserService _userService = userService;
    private readonly IAccountTypesRepository _accountTypesService = accountTypesService;
    private readonly IMapper _mapper = mapper;

    public async Task CreateAsync(AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        await _accountRepository.CreateAsync(account);
    }
    public async Task<List<KeyValueDto>?> GetAccountNamesAsync()
    {
        var userId = _userService.GetUserId();
        return await _accountRepository.GetAccountNamesAsync(userId) as List<KeyValueDto>;
    }
    public async Task<List<KeyValueDto>?> GetAccountTypesNamesAsync()
    {
        var userId = _userService.GetUserId();
        return await _accountTypesService.GetAccountTypesNamesAsync(userId) as List<KeyValueDto>;
    }
    public async Task<AccountDto?> GetAccountByIdAsync(int accountId)
        => _mapper.Map<AccountDto>(
                await _accountRepository.GetAccountByIdAsync(accountId));
    public async Task<List<AccountDto>> GetListAccountsAsync()
        => _mapper.Map<List<AccountDto>>(await _accountRepository.GetAccountsAsync());
    public async Task UpdateAccountAsync(AccountDto account)
        => await _accountRepository.UpdateAccountAsync(_mapper.Map<Account>(account));
    public async Task DeleteAccountAsync(int accountId)
        => await _accountRepository.DeleteAccountAsync(accountId);
}
