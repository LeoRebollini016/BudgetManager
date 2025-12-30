using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Interfaces.Repositories;
using FluentValidation;

namespace BudgetManager.Application.Validators.AccountTypes;

public class AccountTypesValidator: AbstractValidator<AccountTypesDto>
{
    private readonly IAccountTypesRepository _repository;

    public AccountTypesValidator(IAccountTypesRepository repository)
    {
        _repository = repository;

        RuleFor(x => x)
            .CustomAsync(BeUniqueAccountType);
    }
    private async Task BeUniqueAccountType(AccountTypesDto dto, ValidationContext<AccountTypesDto> context, CancellationToken ct)
    {
        if (!context.RootContextData.TryGetValue("UserId", out var userIdObj))
            throw new InvalidOperationException("UserId no fue provisto al validador.");

        var userId = (Guid)userIdObj;
        if(await _repository.ExistAccTypesAsync(userId, dto.Name, ct))
        {
            context.AddFailure(
                nameof(dto.Name),
                "Ya existe un tipo de cuenta con ese nombre."
            );
        }
    }
}
