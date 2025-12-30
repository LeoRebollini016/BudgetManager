
using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Create;

public record CreateAccountRequest(Guid UserId, AccountDto AccountDto) : IRequest<Unit>;
