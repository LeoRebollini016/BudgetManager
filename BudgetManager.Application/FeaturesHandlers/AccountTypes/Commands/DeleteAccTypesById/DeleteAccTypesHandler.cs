using BudgetManager.Domain.Common;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.AccountTypes.Commands.DeleteAccTypesById;

public class DeleteAccTypesHandler(IAccountTypesService accountTypesService) : IRequestHandler<DeleteAccTypesRequest, Result>
{
    private readonly IAccountTypesService _accountTypesService = accountTypesService;

    public async Task<Result> Handle(DeleteAccTypesRequest request, CancellationToken cancellationToken)
       => await _accountTypesService.DeleteAccTypesById(request.UserId, request.Id, cancellationToken);
}