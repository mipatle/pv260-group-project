using Microsoft.AspNetCore.Mvc.Razor;

namespace PV260.ArkFundsTracker.Web.Infrastructure.DependencyInjection;

internal static class PresentationExtensions
{
    public static IServiceCollection AddWebPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationFormats.Insert(0, "/Slices/{1}/Views/{0}.cshtml");
            options.ViewLocationFormats.Insert(1, "/Slices/Common/Views/{0}.cshtml");
        });


        return services;
    }
}