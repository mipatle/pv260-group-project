using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PV260.ArkFundsTracker.Web.Infrastructure.Configuration;
using PV260.ArkFundsTracker.Web.Infrastructure.Logging;
using PV260.ArkFundsTracker.Web.Slices.Common;

namespace PV260.ArkFundsTracker.Web.Slices.Home;

public sealed class HomeController(
    ILogger<HomeController> logger,
    IOptions<ApplicationOptions> applicationOptions)
    : Controller
{
    public IActionResult Index()
    {
        Log.RenderingHomePage(logger);

        var viewModel = new HomePageViewModel
        {
            ApplicationName = applicationOptions.Value.Name
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("home/error")]
    public IActionResult Error()
    {
        var viewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        return View("~/Slices/Common/Views/Error.cshtml", viewModel);
    }
}