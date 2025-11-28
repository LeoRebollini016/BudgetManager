using BudgetManager.Domain.Dtos.Account;
using FluentValidation;

namespace BudgetManager.Application.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(x => x.Balance).NotEmpty().WithMessage("El balance es requerido.");
        RuleFor(x => x.AccountTypeId).NotNull().WithMessage("Es necesario que selecciones un tipo de cuenta.");
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Máximo 1000 caracteres.");
    }
}
