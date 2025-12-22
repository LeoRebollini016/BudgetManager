using AutoMapper;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Create;

public class CreateAccountHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<CreateAccountRequest, Unit>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;
    public async Task<Unit> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(request.AccountDto);
        await _accountService.CreateAsync(account, cancellationToken);
        return Unit.Value;
    }
}