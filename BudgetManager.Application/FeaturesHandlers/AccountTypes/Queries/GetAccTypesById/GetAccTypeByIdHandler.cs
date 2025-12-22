using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Queries.GetAccTypesById;

public class GetAccTypeByIdHandler(IAccountTypesService accountTypesService) : IRequestHandler<GetAccTypeByIdRequest, AccountTypesDto?>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<AccountTypesDto?> Handle(GetAccTypeByIdRequest request, CancellationToken cancellationToken)
        => await _accountTypesService.GetAccTypesById(request.Id, cancellationToken);
}
