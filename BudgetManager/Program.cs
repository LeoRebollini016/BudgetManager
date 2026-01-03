using BudgetManager;
using BudgetManager.Bootstrap.Middlewares;
using BudgetManager.Helpers;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
    opt.ModelMetadataDetailsProviders.Add(new CustomDisplayNameProvider());
});


builder.ConfigureWebApplicationBuilder();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
 //   app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}")
    .WithStaticAssets();

app.Run();
