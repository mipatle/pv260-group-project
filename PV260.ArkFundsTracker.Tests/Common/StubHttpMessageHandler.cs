using System.Net;

namespace PV260.ArkFundsTracker.Tests.Common;

public class StubHttpMessageHandler(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
    : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(responseContent)
        };

        return Task.FromResult(response);
    }
}