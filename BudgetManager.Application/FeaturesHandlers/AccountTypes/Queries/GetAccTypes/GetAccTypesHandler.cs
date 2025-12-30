using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypes;

public class GetAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<GetAccTypesRequest, List<AccountTypesDto>?>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<List<AccountTypesDto>?> Handle(GetAccTypesRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.GetAccountTypesAsync(request.UserId, cancellationToken);
}
