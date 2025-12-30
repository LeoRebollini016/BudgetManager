using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;

public record GetCategoryDeleteInfoRequest(Guid UserId, int CategoryId) : IRequest<CategoryDeleteDto?>;