using AutoMapper;
using BudgetManager.Domain.Dtos;
using BudgetManager.Models;

namespace BudgetManager.Profiles;

public class PresentationProfile: Profile
{
    public PresentationProfile()
    {
        CreateMap<AccountTypesVM, AccountTypesDto>().ReverseMap();
        CreateMap<AccountVM, AccountDto>().ReverseMap();

        CreateMap<AccountVM, AccountCreateDto>().ReverseMap();

        CreateMap<CategoryDto, CategoryVM>();
        CreateMap<CategoryVM, CategoryDto>()
            .ForMember(x => x.Id, x => x.Ignore());

        CreateMap<TransactionDetailDto, TransactionDetailsVM>();
        CreateMap<TransactionCreateVM, TransactionCreateDto>();
    }
}
