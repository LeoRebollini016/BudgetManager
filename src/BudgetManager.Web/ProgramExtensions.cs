using BudgetManager.Bootstrap.Configure;
using BudgetManager.Web.Profiles;
using System.Globalization;

namespace BudgetManager.Web;

public static class ProgramExtensions
{
    public static void ConfigureWebApplicationBuilder(this WebApplicationBuilder builder)
    {
        var cultureInfo = new CultureInfo("es-AR");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        builder.Services.AddProjectsDependency(builder.Configuration);
        builder.Services.AddAutoMapper(cfg => { }, typeof(PresentationProfile).Assembly);
    }
}
