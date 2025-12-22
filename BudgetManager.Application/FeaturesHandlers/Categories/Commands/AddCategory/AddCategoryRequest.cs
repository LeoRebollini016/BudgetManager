using BudgetManager.Domain.Dtos.Category;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Categories.Commands.AddCategory;

public record AddCategoryRequest(CategoryDto CategoryDto) : IRequest<Unit>;