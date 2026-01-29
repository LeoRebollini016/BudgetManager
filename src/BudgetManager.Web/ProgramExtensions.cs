using BudgetManager.Bootstrap.Configure;
using BudgetManager.Web.Profiles;

namespace BudgetManager.Web;

public static class ProgramExtensions
{
    public static void ConfigureWebApplicationBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddProjectsDependency(builder.Configuration);
        builder.Services.AddAutoMapper(cfg => { }, typeof(PresentationProfile).Assembly);
    }
}
