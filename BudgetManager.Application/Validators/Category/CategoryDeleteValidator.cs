using BudgetManager.Domain.Dtos.Category;
using FluentValidation;


namespace BudgetManager.Application.Validators.Category;

public class CategoryDeleteValidator : AbstractValidator<CategoryDeleteDto>
{
    public CategoryDeleteValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
