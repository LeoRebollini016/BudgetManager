using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccounts;

public record GetAccountsRequest() : IRequest<List<AccountDto>>;