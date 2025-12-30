using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;

public class GetTransactionDeleteInfoHandler(ITransactionService transactionService) : IRequestHandler<GetTransactionDeleteInfoRequest, TransactionDeleteDto>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<TransactionDeleteDto> Handle(GetTransactionDeleteInfoRequest request, CancellationToken cancellationToken)
        => await _transactionService.GetTransactionDeleteInfoByIdAsync(request.UserId, request.TransactionId, cancellationToken);
}
