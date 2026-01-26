using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccounts;

public class GetAccountsHandler(IAccountService accountService) : IRequestHandler<GetAccountsRequest, List<AccountDto>>
{
    private readonly IAccountService _accountService = accountService;

    public async Task<List<AccountDto>> Handle(GetAccountsRequest request, CancellationToken cancellationToken)
        => await _accountService.GetAccountListAsync(request.UserId, cancellationToken);
}
