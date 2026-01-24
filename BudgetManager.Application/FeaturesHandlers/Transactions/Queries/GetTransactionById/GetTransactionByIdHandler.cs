using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Transactions.Queries.GetTransactionDeleteInfo;

public class GetTransactionByIdHandler(ITransactionService transactionService) : IRequestHandler<GetTransactionByIdRequest, TransactionDto>
{
    private readonly ITransactionService _transactionService = transactionService;

    public async Task<TransactionDto> Handle(GetTransactionByIdRequest request, CancellationToken cancellationToken)
        => await _transactionService.GetTransactionByIdAsync(request.UserId, request.TransactionId, cancellationToken);
}
