using BudgetManager.Domain.Dtos.Account;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountById;

public record GetAccountByIdRequest(Guid UserId, int AccountId) : IRequest<AccountDto?>;