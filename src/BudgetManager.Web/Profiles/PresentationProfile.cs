using AutoMapper;
using BudgetManager.Domain.Dtos.Account;
using BudgetManager.Domain.Dtos.AccountTypes;
using BudgetManager.Domain.Dtos.Category;
using BudgetManager.Domain.Dtos.Report;
using BudgetManager.Domain.Dtos.Transaction;
using BudgetManager.Web.Models;
using BudgetManager.Web.Models.Account;
using BudgetManager.Web.Models.AccountTypes;
using BudgetManager.Web.Models.Category;
using BudgetManager.Web.Models.Report;
using BudgetManager.Web.Models.Transaction;

namespace BudgetManager.Web.Profiles;

public class PresentationProfile: Profile
{
    public PresentationProfile()
    {

        CreateMap<AccountDto, AccountListVM>();
        CreateMap<AccountFormVM, AccountDto>().ReverseMap();
        CreateMap<AccountDto, AccountDeleteVM>();

        CreateMap<AccountTypesListVM, AccountTypesDto>().ReverseMap();
        CreateMap<AccountTypesFormVM, AccountTypesDto>().ReverseMap();
        CreateMap<AccountTypesDto, AccountTypesDeleteVM>();

        CreateMap<CategoryDto, CategoryListVM>();
        CreateMap<CategoryDto, CategoryFormVM>().ReverseMap();
        CreateMap<CategoryDeleteDto, CategoryDeleteVM>();

        CreateMap<TransactionDetailDto, TransactionListVM>();
        CreateMap<TransactionFormVM, TransactionCreateDto>();
        CreateMap<TransactionDto, TransactionFormVM>();
        CreateMap<TransactionDto, TransactionDeleteVM>();

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
