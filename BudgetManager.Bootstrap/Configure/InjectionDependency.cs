using BudgetManager.Application.Services;
using BudgetManager.Context;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Interfaces.Repositories;
using BudgetManager.Services;
using BudgetManager.Services.AccountTypesRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BudgetManager.Application.Profiles;
using FluentValidation;
using BudgetManager.Infraestructure.Repositories;
using BudgetManager.Application.Validators.Account;
using BudgetManager.Application.Validators.AccountTypes;
using BudgetManager.Application.Validators.Category;
using System.Reflection;
using BudgetManager.Domain.Interfaces;

namespace BudgetManager.Bootstrap.Configure;

public static class InjectionDependency
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAccountTypesService, AccountTypesService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<IReportService, ReportService>();
        services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountTypesValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountValidator>();
        services.AddValidatorsFromAssemblyContaining<CategoryDeleteValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("BudgetManager.Application")));
        return services;
    }
    public static IServiceCollection AddInfraestructureDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddTransient<IAccountTypesRepository, AccountTypesRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IReportRepository, ReportRepository>();

        return services;
    }
    public static IServiceCollection AddProjectsDependency(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDependency(services);
        AddInfraestructureDependency(services, configuration);
        return services;
    }
}
