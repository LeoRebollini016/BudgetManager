using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.OrderListAccTypes;

public class OrderAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<OrderAccTypesRequest, bool>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<bool> Handle(OrderAccTypesRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.OrderListAccTypes(request.UserId, request.Ids, cancellationToken);
}