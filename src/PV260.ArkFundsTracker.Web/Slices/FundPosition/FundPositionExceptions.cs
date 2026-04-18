namespace PV260.ArkFundsTracker.Web.Slices.FundPosition;

public class DataNotLatestException(DateOnly received, DateOnly expected)
    : Exception($"Data is not latest. Received: {received}, Expected: {expected}");

public class DataInconsistentException(string message) : Exception(message);

public class DataUnavailableException(string message): Exception(message);