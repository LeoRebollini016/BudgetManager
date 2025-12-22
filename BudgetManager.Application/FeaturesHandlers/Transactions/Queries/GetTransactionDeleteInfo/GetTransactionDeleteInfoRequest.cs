using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;

public record GetTransactionDeleteInfoRequest(int TransactionId) : IRequest<TransactionDeleteDto>;
