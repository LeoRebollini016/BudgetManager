using BudgetManager.Domain.Dtos;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;

public record GetAccountNamesRequest(Guid UserId) : IRequest<List<KeyValueDto>?>;