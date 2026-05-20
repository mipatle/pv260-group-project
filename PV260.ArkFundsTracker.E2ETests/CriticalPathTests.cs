using Microsoft.Playwright;

namespace PV260.ArkFundsTracker.E2ETests
{
    [TestFixture]
    public class CriticalPathTests : PageTest
    {
        private string _baseUrl;
        private string[] _seededDates = ["15.04.2026", "16.04.2026"];

        [SetUp]
        public void Setup()
        {
            _baseUrl = Environment.GetEnvironmentVariable("E2E_BASE_URL") ?? "http://localhost:8080";
        }
        
        [Test]
        public async Task FullUserFlow_Login_Ingest_AndViewData_Successfully()
        {
            // 1. --- LOGIN FLOW ---
            await Page.GotoAsync($"{_baseUrl}/Auth/Login");

            await Page.GetByLabel("Email").FillAsync("admin@example.com");
            await Page.GetByLabel("Password").FillAsync("Admin123");
            
            await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
            
            await Expect(Page).ToHaveURLAsync($"{_baseUrl}/");
            await Expect(Page.GetByText("View Holdings")).ToBeVisibleAsync();
            
            // 2. --- INGEST FLOW ---
            await Page.GetByRole(AriaRole.Link, new() { Name = "View Holdings" }).ClickAsync();
            await Expect(Page).ToHaveURLAsync($"{_baseUrl}/FundPosition");

            await Page.GetByRole(AriaRole.Button, new() { Name = "Download Latest Data" }).ClickAsync();

            var table = Page.GetByRole(AriaRole.Table);
            await Expect(table).ToBeVisibleAsync();

            var successBadge = Page.GetByText("Source: Freshly fetched data");
            await Expect(successBadge).ToBeVisibleAsync();

            // 3. --- VIEW DATA FLOW ---
            await Page.GetByRole(AriaRole.Link, new() { Name = "Timestamp compare" }).ClickAsync();

            var dateLocator = Page.Locator(".badge strong");
            string actualDate = await dateLocator.InnerTextAsync();

            Assert.That(_seededDates, Does.Not.Contain(actualDate));
            
            var comparisonTable = Page.GetByRole(AriaRole.Table);
            await Expect(table).ToBeVisibleAsync();
        }
    }
}