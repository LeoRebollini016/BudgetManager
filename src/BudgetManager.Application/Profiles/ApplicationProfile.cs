using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Entities;

namespace BudgetManager.Application.Profiles;

public class ApplicationProfile: Profile
{
    public ApplicationProfile()
    {
        CreateMap<AccountTypes, AccountTypesDto>().ReverseMap();
        CreateMap<Account, AccountDto>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();

    }
}
