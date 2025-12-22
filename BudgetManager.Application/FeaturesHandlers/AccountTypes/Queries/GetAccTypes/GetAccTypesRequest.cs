using BudgetManager.Domain.Dtos.AccountTypes;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypes;

public record GetAccTypesRequest(int userId) : IRequest<List<AccountTypesDto>?>;