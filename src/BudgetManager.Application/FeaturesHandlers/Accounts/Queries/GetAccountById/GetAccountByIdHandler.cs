using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Interfaces.Services;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountById;

public class GetAccountByIdHandler(IAccountService accountService, IMapper mapper) : IRequestHandler<GetAccountByIdRequest, AccountDto?>
{
    private readonly IAccountService _accountService = accountService;
    private readonly IMapper _mapper = mapper;

    public async Task<AccountDto?> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
        => _mapper.Map<AccountDto?>(
            await _accountService.GetAccountByIdAsync(request.UserId, request.AccountId, cancellationToken));
}