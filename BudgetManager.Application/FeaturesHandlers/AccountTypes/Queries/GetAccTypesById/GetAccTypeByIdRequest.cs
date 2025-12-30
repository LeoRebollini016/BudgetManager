using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesById;

public record GetAccTypeByIdRequest(Guid UserId, int Id) : IRequest<AccountTypesDto?>;