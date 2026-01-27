using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionList;

public class GetTransactionListHandler(ITransactionService transactionService) : IRequestHandler<GetTransactionListRequest, List<TransactionDetailDto>>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<List<TransactionDetailDto>> Handle(GetTransactionListRequest request, CancellationToken cancellationToken)
        => await _transactionService.GetTransactionListAsync(request.UserId, cancellationToken);
}