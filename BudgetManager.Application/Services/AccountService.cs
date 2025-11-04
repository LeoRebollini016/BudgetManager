using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;

namespace BudgetManager.Application.Services;

public class AccountService(IAccountRepositories accountRepository, IUserService userService, IAccountTypesRepositories accountTypesService, IMapper mapper) : IAccountService
{
    private readonly IAccountRepositories _accountRepository = accountRepository;
    private readonly IUserService _userService = userService;
    private readonly IAccountTypesRepositories _accountTypesService = accountTypesService;
    private readonly IMapper _mapper = mapper;

    public async Task CreateAsync(AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        await _accountRepository.CreateAsync(account);
    }
    public async Task<List<ListNameAccountTypesDto>> GetAccountTypesNamesAsync()
    {
        var userId = _userService.GetUserId();
        return await _accountTypesService.GetAccTypesNamesByUserAsync(userId) as List<ListNameAccountTypesDto>;
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
