using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;

public record DeleteTransactionRequest(int TransactionId) : IRequest<Unit>;
