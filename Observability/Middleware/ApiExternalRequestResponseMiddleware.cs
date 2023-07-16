using System.Diagnostics;

public class ApiExternalRequestResponseMiddleware : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        string requestId = RequestIdContext.Current;

        await CaptureRequest(requestId, request);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        await CaptureResponse(requestId, response, elapsedMilliseconds);

        return response;
    }

    private async Task CaptureRequest(string requestId, HttpRequestMessage request)
    {
        string requestUri = request.RequestUri.ToString();

        string requestBody = null;
        if (request.Content != null)
        {
            requestBody = await request.Content.ReadAsStringAsync();
        }

        RequestDataStorage.StoreApiExternalRequestData(requestId, request);
    }

    private async Task CaptureResponse(string requestId, HttpResponseMessage response, long elapsedMilliseconds)
    {
        string responseBody = null;
        if (response.Content != null)
        {
            responseBody = await response.Content.ReadAsStringAsync();
        }

        RequestDataStorage.StoreApiExternalResponseData(requestId, response, elapsedMilliseconds);
    }
}
