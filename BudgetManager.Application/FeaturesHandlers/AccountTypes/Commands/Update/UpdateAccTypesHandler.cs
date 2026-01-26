using BudgetManager.Domain.Common;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.Update;

public class UpdateAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<UpdateAccTypesRequest, Result>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<Result> Handle(UpdateAccTypesRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.Update(request.UserId, request.AccountTypesDto, cancellationToken);
}
