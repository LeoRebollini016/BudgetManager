using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Queries.GetCategoryDeleteInfo;

public record GetCategoryDeleteInfoRequest(int CategoryId) : IRequest<CategoryDeleteDto?>;