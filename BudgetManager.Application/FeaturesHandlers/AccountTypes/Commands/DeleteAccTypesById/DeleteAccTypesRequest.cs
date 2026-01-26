using BudgetManager.Domain.Common;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;

public record DeleteAccTypesRequest(Guid UserId, int Id): IRequest<Result>;