using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.CreateAccount;

public class CreateAccountHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<CreateAccountRequest, Result>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(request.AccountDto);
        account.UserId = request.UserId;

        return await _accountService.CreateAsync(account, cancellationToken);
    }
}