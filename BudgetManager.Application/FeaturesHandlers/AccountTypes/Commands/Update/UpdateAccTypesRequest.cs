using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;

public record UpdateAccTypesRequest(AccountTypesDto AccountTypesDto) : IRequest<Unit>;