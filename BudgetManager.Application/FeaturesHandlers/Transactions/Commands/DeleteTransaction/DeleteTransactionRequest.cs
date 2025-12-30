using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionRequest(Guid UserId, int TransactionId) : IRequest<Unit>;
