using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.ExistAccTypes;

public record ExistAccTypesRequest(Guid UserId, string Name) : IRequest<bool>;