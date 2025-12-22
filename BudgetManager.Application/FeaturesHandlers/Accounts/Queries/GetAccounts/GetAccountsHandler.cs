using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccounts;

public class GetAccountsHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<GetAccountsRequest, List<AccountDto>>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<AccountDto>> Handle(GetAccountsRequest request, CancellationToken cancellationToken)
        => _mapper.Map<List<AccountDto>>(
                await _accountService.GetAccountListAsync(cancellationToken));
}
