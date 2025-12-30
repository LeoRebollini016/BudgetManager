using AutoMapper;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Update;

public class UpdateAccountHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<UpdateAccountRequest, Unit>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<Unit> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(request.AccountDto);
        account.UserId = request.UserId;
        await _accountService.UpdateAccountAsync(account, cancellationToken);
        return Unit.Value;
    }
}