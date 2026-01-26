using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.UpdateCategory;

public record UpdateCategoryRequest(Guid UserId, CategoryDto CategoryDto) : IRequest<Result>;
