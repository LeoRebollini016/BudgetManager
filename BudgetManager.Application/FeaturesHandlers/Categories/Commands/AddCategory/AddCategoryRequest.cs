using BudgetManager.Domain.Common;
using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;

public record AddCategoryRequest(Guid UserId, CategoryDto CategoryDto) : IRequest<Result>;