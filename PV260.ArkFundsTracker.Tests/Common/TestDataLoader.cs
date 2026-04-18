namespace PV260.ArkFundsTracker.Tests.Common;

public static class TestDataLoader
{
    public static string LoadCsv(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", fileName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Test data file not found: {path}");    
        }

        return File.ReadAllText(path);
    }

    ///<summary>
    /// Loads a CSV file and replaces the placeholder {{TODAY}} with the current date in M/d/yyyy format.
    /// </summary>
    public static string LoadCsvWithToday(string fileName)
    {
        var content = LoadCsv(fileName);

        var today = DateOnly
            .FromDateTime(DateTime.Today)
            .ToString("M/d/yyyy");

        return content.Replace("{{TODAY}}", today);
    }
}