using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Dtos.Transaction;
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
        CreateMap<CategoryDeleteDto, CategoryDeleteVM>();

        CreateMap<TransactionDetailDto, TransactionDetailsVM>();
        CreateMap<TransactionCreateVM, TransactionCreateDto>();
        CreateMap<TransactionDeleteDto, TransactionDeleteVM>();

        CreateMap<ReportViewModel, MonthlyReportFilterDto>()
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month!.Value))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId));

        CreateMap<ReportViewModel, DateRangeReportFilterDto>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId)).ReverseMap();

        CreateMap<MonthlyReportResultDto, ReportViewModel>();
        CreateMap<DateRangeReportResultDto, ReportViewModel>();
        CreateMap<CategoryReportResultDto, ReportViewModel>();
    }
}
