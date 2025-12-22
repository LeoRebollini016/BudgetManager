using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Create;

public record CreateAccTypesRequest(AccountTypesDto AccountTypesDto) : IRequest<Unit>;
