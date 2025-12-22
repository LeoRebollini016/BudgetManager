using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;

public class UpdateAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<UpdateAccTypesRequest, Unit>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<Unit> Handle(UpdateAccTypesRequest request, CancellationToken cancellationToken)
    {
        await _accountTypesService.Update(request.AccountTypesDto, cancellationToken);
        return Unit.Value;
    }
}
