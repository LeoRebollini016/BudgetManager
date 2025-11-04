using AutoMapper;
using BudgetManager.Domain.Dtos;
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
