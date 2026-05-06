using Microsoft.AspNetCore.Mvc;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavController(TimestampNavService timestampNavService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(DateOnly? firstDate)
    {
        try
        {
            var viewModel = await timestampNavService.FillTimestampNavViewModel(firstDate, HttpContext.RequestAborted);
            return View(viewModel);
        }
        catch (OperationCanceledException)
        {
            return RedirectToAction(nameof(Index));
        }
        catch (DataWithWrongValueException ex)
        {
            TempData["ErrorMessage"] = "Fetched data has wrong values:" + ex.Message;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Unexpected exception happened: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}