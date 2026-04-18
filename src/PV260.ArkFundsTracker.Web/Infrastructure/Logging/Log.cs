namespace PV260.ArkFundsTracker.Web.Infrastructure.Logging;

public static partial class Log
{
    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "Error at row {RowNumber}: {row}\n{ErrorMessage}")]
    public static partial void RowParsingError(ILogger logger, int rowNumber, string row, string errorMessage);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Error,
        Message = "Error at during parsing the CSV. {ErrorMessage}")]
    public static partial void CsvParsingError(ILogger logger, string errorMessage = "");

    [LoggerMessage(
        EventId = 1003,
        Level = LogLevel.Error,
        Message = "Error at fetching the CSV. {ErrorMessage}")]
    public static partial void HttpFetchingError(ILogger logger, string errorMessage = "");

    [LoggerMessage(
        EventId = 1004,
        Level = LogLevel.Information,
        Message = "Starting the CronJob for date {Date} - fetching the data.")]
    public static partial void CronJobStart(ILogger logger, DateOnly date);

    [LoggerMessage(
        EventId = 1005,
        Level = LogLevel.Information,
        Message = "CronJob for date {Date} finished succesfully.")]
    public static partial void CronJobFinished(ILogger logger, DateOnly date);

    [LoggerMessage(
        EventId = 1006,
        Level = LogLevel.Error,
        Message = "CronJob for date {Date} failed. Reason: {ErrorMessage}")]
    public static partial void CronJobFailed(ILogger logger, DateOnly date, string errorMessage = "");
}