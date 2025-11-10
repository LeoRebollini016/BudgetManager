using BudgetManager.Domain.Dtos;
using BudgetManager.Interfaces.Repositories;
using FluentValidation;

namespace BudgetManager.Validations;

public class AccountTypesValidator: AbstractValidator<AccountTypesDto>
{
    private readonly IAccountTypesRepository _repository;

    public AccountTypesValidator(IAccountTypesRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo nombre es requerido.");

        RuleFor(x => x)
            .MustAsync(BeUniqueAccountType)
            .WithMessage("Ya existe un tipo de cuenta con ese nombre.");
    }
    private async Task<bool> BeUniqueAccountType(AccountTypesDto accountType, CancellationToken ct)
    {
        return !await _repository.ExistAccTypesAsync(accountType.Name, accountType.UserId);
    }
}
