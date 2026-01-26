using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Create;

public record CreateAccTypesRequest(Guid UserId, AccountTypesDto AccountTypesDto) : IRequest<Result>;
