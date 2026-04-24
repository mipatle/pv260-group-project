using Microsoft.AspNetCore.Mvc;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavController : Controller
{
    private readonly TimestampNavService _timestampNavService;

    public TimestampNavController(TimestampNavService timestampNavService)
    {
        _timestampNavService = timestampNavService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateOnly? firstDate)
    {
        var viewModel = await _timestampNavService.FillTimestampNavViewModel(firstDate);
        return View(viewModel);
    }
}