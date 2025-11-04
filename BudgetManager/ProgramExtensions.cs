using BudgetManager.Bootstrap.Configure;
using BudgetManager.Profiles;

namespace BudgetManager;

public static class ProgramExtensions
{
    public static void ConfigureWebApplicationBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddProjectsDependency(builder.Configuration);
        builder.Services.AddAutoMapper(cfg => { }, typeof(PresentationProfile).Assembly);
    }
}
