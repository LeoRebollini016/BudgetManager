using BudgetManager.Domain.Common;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler(ITransactionService transactionService) : IRequestHandler<DeleteTransactionRequest, Result>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<Result> Handle(DeleteTransactionRequest request, CancellationToken cancellationToken)
        => await _transactionService.DeleteTransactionByIdAsync(request.UserId, request.TransactionId, cancellationToken);
}