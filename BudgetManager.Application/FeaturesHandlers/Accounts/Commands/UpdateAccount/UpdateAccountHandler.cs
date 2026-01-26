using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<UpdateAccountRequest, Result>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(request.AccountDto);
        account.UserId = request.UserId;
        return await _accountService.UpdateAccountAsync(account, cancellationToken);
    }
}