using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Update;

public record UpdateAccountRequest(AccountDto AccountDto) : IRequest<Unit>;