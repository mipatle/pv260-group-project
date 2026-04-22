using Microsoft.EntityFrameworkCore;
using PV260.ArkFundsTracker.Web.Infrastructure.Data;
using PV260.ArkFundsTracker.Web.Infrastructure.DependencyInjection;
using PV260.ArkFundsTracker.Web.Slices.CronFetching;
using PV260.ArkFundsTracker.Web.Slices.FundPosition;
using PV260.ArkFundsTracker.Web.Slices.FundPosition.Validators;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav;
using PV260.ArkFundsTracker.Web.Slices.TimestampNav.TimestampCompare;

var builder = WebApplication.CreateBuilder(args);
const string errorPath = "/home/error";

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddDebug();
}

// Add services to the container.
builder.Services
    .AddWebPresentation()
    .AddApplicationOptions(builder.Configuration)
    .AddScoped<FundPositionsService>()
    .AddScoped<IFundPositionValidator, FundPositionValidator>()
    .AddScoped<TimestampNavService>()
    .AddScoped<TimestampCompareService>()
    .AddHttpClient()
    .AddHostedService<CronJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }
// else

// Auto migration
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    app.UseExceptionHandler(errorPath);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();