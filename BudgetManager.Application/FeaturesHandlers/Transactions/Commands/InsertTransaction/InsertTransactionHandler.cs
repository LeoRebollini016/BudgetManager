using AutoMapper;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;

public class InsertTransactionHandler(ITransactionService transactionService) : IRequestHandler<InsertTransactionRequest, Unit>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<Unit> Handle(InsertTransactionRequest request, CancellationToken cancellationToken)
    {
        await _transactionService.InsertTransactionAsync(request.TransactionDto, cancellationToken);
        return Unit.Value;
    }
}
