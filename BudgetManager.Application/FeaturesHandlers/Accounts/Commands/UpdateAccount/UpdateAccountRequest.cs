using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.UpdateAccount;

public record UpdateAccountRequest(Guid UserId, AccountDto AccountDto) : IRequest<Unit>;