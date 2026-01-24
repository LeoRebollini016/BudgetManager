using AutoMapper;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler(IAccountService accountService) : IRequestHandler<DeleteAccountRequest, Unit>
{
    private readonly IAccountService _accountService = accountService;

    public async Task<Unit> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        await _accountService.DeleteAccountAsync(request.UserId, request.AccountId, cancellationToken);
        return Unit.Value;
    }
}
