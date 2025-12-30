using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;

public record UpdateTransactionRequest(Guid UserId, TransactionCreateDto TransactionDto) : IRequest<Unit>;