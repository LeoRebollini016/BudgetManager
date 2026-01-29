using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;

public record GetTransactionByIdRequest(Guid UserId, int TransactionId) : IRequest<TransactionDto?>;