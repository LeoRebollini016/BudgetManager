
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.CreateAccount;

public record CreateAccountRequest(Guid UserId, AccountDto AccountDto) : IRequest<Result>;
