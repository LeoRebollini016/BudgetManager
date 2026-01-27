using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionList;

public record GetTransactionListRequest(Guid UserId) : IRequest<List<TransactionDetailDto>>;