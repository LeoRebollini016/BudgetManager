using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Delete;

public record DeleteAccountRequest(Guid UserId, int AccountId) : IRequest<Unit>;
