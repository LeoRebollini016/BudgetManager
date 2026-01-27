using BudgetManager.Domain.Dtos;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryNames;

public record GetCategoryNamesRequest(Guid UserId) : IRequest<List<KeyValueDto>>;
