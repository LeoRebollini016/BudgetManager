using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Commands.Delete;

public record DeleteAccountRequest(int AccountId) : IRequest<Unit>;
