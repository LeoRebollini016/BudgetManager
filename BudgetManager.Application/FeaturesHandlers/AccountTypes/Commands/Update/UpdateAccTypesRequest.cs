using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;

public record UpdateAccTypesRequest(Guid UserId, AccountTypesDto AccountTypesDto) : IRequest<Unit>;