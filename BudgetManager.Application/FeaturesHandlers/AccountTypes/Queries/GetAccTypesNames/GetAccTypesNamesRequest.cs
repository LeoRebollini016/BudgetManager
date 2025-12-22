using BudgetManager.Domain.Dtos;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesNames;

public record GetAccTypesNamesRequest() : IRequest<List<KeyValueDto>?>;
