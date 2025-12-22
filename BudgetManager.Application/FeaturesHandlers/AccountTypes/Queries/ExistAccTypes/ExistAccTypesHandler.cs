using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.ExistAccTypes;

public class ExistAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<ExistAccTypesRequest, bool>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<bool> Handle(ExistAccTypesRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.ExistAccTypes(request.Name, cancellationToken);
}