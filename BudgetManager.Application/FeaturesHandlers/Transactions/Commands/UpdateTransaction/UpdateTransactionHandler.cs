using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler(ITransactionService transactionService) : IRequestHandler<UpdateTransactionRequest, Unit>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<Unit> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        await _transactionService.UpdateTransactionAsync(request.UserId, request.TransactionDto, cancellationToken);
        return Unit.Value;
    }
}