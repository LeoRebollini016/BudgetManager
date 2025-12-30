using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;

public record GetTransactionDeleteInfoRequest(Guid UserId, int TransactionId) : IRequest<TransactionDeleteDto>;
