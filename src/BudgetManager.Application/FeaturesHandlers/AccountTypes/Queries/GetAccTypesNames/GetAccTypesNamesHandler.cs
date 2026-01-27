using BudgetManager.Domain.Dtos;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesNames;

public class GetAccTypesNamesHandler(IAccountTypesService accountTypesService) : IRequestHandler<GetAccTypesNamesRequest, List<KeyValueDto>?>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<List<KeyValueDto>?> Handle(GetAccTypesNamesRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.GetAccountTypesNamesAsync(request.UserId, cancellationToken);
}