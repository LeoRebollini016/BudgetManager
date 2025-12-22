using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.ExistAccTypes;

public record ExistAccTypesRequest(string Name) : IRequest<bool>;