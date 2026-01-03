using BudgetManager.Application.Profiles;
using BudgetManager.Application.Services;
using BudgetManager.Application.Validators.Account;
using BudgetManager.Application.Validators.AccountTypes;
using BudgetManager.Application.Validators.Category;
using BudgetManager.Context;
using BudgetManager.Domain.Interfaces;
using BudgetManager.Domain.Interfaces.Repositories;
using BudgetManager.Domain.Interfaces.Services;
using BudgetManager.Infraestructure.Identity;
using BudgetManager.Infraestructure.Repositories;
using BudgetManager.Interfaces.Repositories;
using BudgetManager.Services;
using BudgetManager.Services.AccountTypesRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BudgetManager.Bootstrap.Configure;

public static class InjectionDependency
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
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
        services.AddDbContext<UserIdentityDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddTransient<IAccountTypesRepository, AccountTypesRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IReportRepository, ReportRepository>();

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserIdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme);
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/Login";
            options.AccessDeniedPath = "/User/AccessDenied";
        });

        return services;
    }
    public static IServiceCollection AddProjectsDependency(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDependency(services);
        AddInfraestructureDependency(services, configuration);
        return services;
    }
}
