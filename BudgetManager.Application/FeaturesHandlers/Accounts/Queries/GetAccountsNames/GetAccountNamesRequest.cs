using BudgetManager.Domain.Dtos;
using MediatR;

namespace BudgetManager.Application.FeaturesHandlers.Accounts.Queries.GetAccountsNames;

public record GetAccountNamesRequest() : IRequest<List<KeyValueDto>?>;