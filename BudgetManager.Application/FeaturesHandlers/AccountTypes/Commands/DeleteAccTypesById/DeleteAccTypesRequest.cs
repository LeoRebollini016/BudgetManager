using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;

public record DeleteAccTypesRequest(int Id): IRequest<bool>;