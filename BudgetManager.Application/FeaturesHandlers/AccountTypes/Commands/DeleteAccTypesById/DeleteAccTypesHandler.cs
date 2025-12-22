using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;

public class DeleteAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<DeleteAccTypesRequest, bool>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<bool> Handle(DeleteAccTypesRequest request, CancellationToken cancellationToken)
       => await _accountTypesService.DeleteAccTypesById(request.Id, cancellationToken);
}