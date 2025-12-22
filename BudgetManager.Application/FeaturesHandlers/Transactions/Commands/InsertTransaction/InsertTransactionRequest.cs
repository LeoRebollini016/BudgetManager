using BudgetManager.Domain.Dtos.Transaction;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;

public record InsertTransactionRequest(TransactionCreateDto TransactionDto) : IRequest<Unit>;