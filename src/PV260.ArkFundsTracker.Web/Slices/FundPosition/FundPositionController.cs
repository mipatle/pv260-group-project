using Microsoft.AspNetCore.Mvc;

namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class FundPositionController : Controller
{
    private readonly FundPositionsService _service;

    public FundPositionController(FundPositionsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateOnly? date)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        var positions = await _service.GetHistory(targetDate);

        var viewModel = new FundHoldingsViewModel
        {
            Positions = positions,
            SelectedDate = targetDate,
            IsFromDatabase = TempData["IsFetched"] == null
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Refresh()
    {
        try
        {
            await _service.FetchAndSaveLatest();
            TempData["IsFetched"] = true;
        }
        catch (DataInconsistentException ex)
        {
            TempData["ErrorMessage"] = "Fetched data are inconsistent: " + ex.Message;
        }
        catch (DataNotLatestException ex)
        {
            TempData["ErrorMessage"] = "Fetched data is not the latest data: " + ex.Message;
        }
        catch (DataUnavailableException ex)
        {
            TempData["ErrorMessage"] = "Unable to refresh the latest data: " + ex.Message;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Unexpected exception happened: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}