using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;

public static class RequestDataStorage
{
    private static ConcurrentDictionary<string, List<RequestEntry>> _requestData = new ConcurrentDictionary<string, List<RequestEntry>>();

    public static void StoreRequestData(string requestId, HttpRequest incomingRequest)
    {
        var requestData = new RequestEntry
        {
            RequestType = "API Request",
            Path = incomingRequest.Path,
            Method = incomingRequest.Method,
            Headers = incomingRequest.Headers.ToString(),
            Body = incomingRequest.Body.ToString(),
            Query = incomingRequest.Query.ToString(),
        };

        var requestEntries = new List<RequestEntry> { requestData };
        _requestData.TryAdd(requestId, requestEntries);
    }

    public static void StoreApiExternalRequestData(string requestId, HttpRequestMessage apiRequest)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            var requestData = new RequestEntry
            {
                RequestType = "API Externa Request",
                Path = apiRequest.RequestUri.ToString(),
                Method = apiRequest.Method.ToString(),
                Headers = apiRequest.Headers.ToString(),
                Body = apiRequest?.Content?.ToString(),
            };

            requestEntries.Add(requestData);
        }
    }

    public static void StoreApiExternalResponseData(string requestId, HttpResponseMessage apiResponse, long responseTime)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            var responseData = new RequestEntry
            {
                RequestType = "API Externa Response",
                Path = apiResponse.RequestMessage.RequestUri.ToString(),
                StatusCode = (int)apiResponse.StatusCode,
                IsSuccess = apiResponse.IsSuccessStatusCode,
                TimeTaken = responseTime,
                Body = apiResponse.Content.ToString(),
                Headers= apiResponse.Headers.ToString(),
            };

            requestEntries.Add(responseData);
        }
    }

    public static void StoreResponseData(string requestId, HttpResponse response, long responseTime)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            var responseData = new RequestEntry
            {
                RequestType = "API Response",
                Path = response.HttpContext.Request.Path,
                StatusCode = (int?)response.StatusCode,
                TimeTaken = responseTime,
                Body = response.Body.ToString(),
                ContentType = response.ContentType,
                Headers = response.Headers.ToString(),
            };

            requestEntries.Add(responseData);
        }
    }

    public static List<RequestEntry> GetRequestDataById(string requestId)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            return requestEntries.OrderBy(entry => entry.RequestType).ToList();
        }

        return null;
    }

    public static void RemoveRequestData(string requestId)
    {
        _requestData.TryRemove(requestId, out _);
    }
}
