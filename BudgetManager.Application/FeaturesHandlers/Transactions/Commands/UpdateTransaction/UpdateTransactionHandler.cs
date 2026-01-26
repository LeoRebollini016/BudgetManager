using BudgetManager.Domain.Common;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler(ITransactionService transactionService) : IRequestHandler<UpdateTransactionRequest, Result>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<Result> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
        => await _transactionService.UpdateTransactionAsync(request.UserId, request.TransactionDto, cancellationToken);
}