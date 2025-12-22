using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.OrderListAccTypes;

public record OrderAccTypesRequest(IEnumerable<int> Ids) : IRequest<bool>;