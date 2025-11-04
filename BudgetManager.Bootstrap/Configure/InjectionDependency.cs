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
using BudgetManager.Validations;
using FluentValidation;
using BudgetManager.Application.Validators;
using BudgetManager.Infraestructure.Repositories;

namespace BudgetManager.Bootstrap.Configure;

public static class InjectionDependency
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAccountTypesService, AccountTypesService>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<AccountTypesValidator>();
        services.AddValidatorsFromAssemblyContaining<AccountValidator>();
        return services;
    }
    public static IServiceCollection AddInfraestructureDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddTransient<IAccountTypesRepositories, AccountTypesRepositories>();
        services.AddTransient<IAccountRepositories, AccountRepositories>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();

        return services;
    }
    public static IServiceCollection AddProjectsDependency(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDependency(services);
        AddInfraestructureDependency(services, configuration);
        return services;
    }
}
