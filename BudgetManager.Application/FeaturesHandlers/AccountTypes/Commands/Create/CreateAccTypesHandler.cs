using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Create;

public class CreateAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<CreateAccTypesRequest, Unit>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<Unit> Handle(CreateAccTypesRequest request, CancellationToken cancellationToken)
    {
        await _accountTypesService.Create(request.UserId, request.AccountTypesDto, cancellationToken);
        return Unit.Value;
    }
}
