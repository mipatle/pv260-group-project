using Microsoft.AspNetCore.Mvc;

namespace PV260.ArkFundsTracker.Web.Slices.TimestampNav;

public class TimestampNavController(TimestampNavService timestampNavService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(DateOnly? firstDate)
    {
        var dateList = new List<DateOnly>();
        try
        {
            dateList = await timestampNavService.GetFirstDateList(HttpContext.RequestAborted);
            var viewModel =
                await timestampNavService.FillTimestampNavViewModel(firstDate, dateList, HttpContext.RequestAborted);
            return View(viewModel);
        }
        catch (OperationCanceledException)
        {
            TempData["ErrorMessage"] = "Operation was canceled.";
        }
        catch (DataWithWrongValueException ex)
        {
            TempData["ErrorMessage"] = "Fetched data has wrong values: " + ex.Message;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Unexpected exception happened: " + ex.Message;
        }

        return View(new TimestampNavViewModel { DateList = dateList });
    }
}