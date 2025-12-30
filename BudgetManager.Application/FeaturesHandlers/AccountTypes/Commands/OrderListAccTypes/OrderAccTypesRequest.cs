using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.OrderListAccTypes;

public record OrderAccTypesRequest(Guid UserId, IEnumerable<int> Ids) : IRequest<bool>;