using AutoMapper;
using BudgetManager.Domain.Common;
using BudgetManager.Domain.Entities;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Commands.InsertTransaction;

public class InsertTransactionHandler(ITransactionService transactionService) : IRequestHandler<InsertTransactionRequest, Result>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<Result> Handle(InsertTransactionRequest request, CancellationToken cancellationToken)
        => await _transactionService.InsertTransactionAsync(request.UserId, request.TransactionDto, cancellationToken);
}
