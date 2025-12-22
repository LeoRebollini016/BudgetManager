using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;

public class GetAccountNamesHandler(IAccountService accountService) : IRequestHandler<GetAccountNamesRequest, List<KeyValueDto>?>
{
    private readonly IAccountService _accountService = accountService;

    public async Task<List<KeyValueDto>?> Handle(GetAccountNamesRequest request, CancellationToken cancellationToken)
        => await _accountService.GetAccountNamesAsync(cancellationToken);
}