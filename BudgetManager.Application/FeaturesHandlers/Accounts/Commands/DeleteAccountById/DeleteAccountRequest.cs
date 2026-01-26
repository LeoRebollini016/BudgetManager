using BudgetManager.Domain.Common;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.DeleteAccount;

public record DeleteAccountRequest(Guid UserId, int AccountId) : IRequest<Result>;
