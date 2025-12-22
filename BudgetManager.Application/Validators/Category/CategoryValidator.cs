using BudgetManager.Domain.Constants.Enum;
using BudgetManager.Domain.Dtos.Category;
using FluentValidation;

namespace BudgetManager.Application.Validators.Category;

public class CategoryValidator: AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es requerido.");
        RuleFor(c => c.OperationTypeId).NotEmpty();
        RuleFor(c => c.OperationType).IsInEnum();
    }
}
