using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Net;

public static class RequestDataStorage
{
    private static ConcurrentDictionary<string, List<RequestEntry>> _requestData = new ConcurrentDictionary<string, List<RequestEntry>>();

    public static void StoreRequestData(string requestId, HttpRequest incomingRequest)
    {
        var requestData = new RequestEntry
        {
            Origem = "API Request",
            Path = incomingRequest.Path,
            Method = incomingRequest.Method,
        };

        var requestEntries = new List<RequestEntry> { requestData };
        _requestData.TryAdd(requestId, requestEntries);
    }

    public static void StoreApiRequestData(string requestId, HttpRequestMessage apiRequest)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            var requestData = new RequestEntry
            {
                Origem = "API Externa Request",
                Path = apiRequest.RequestUri.ToString(),
                Method = apiRequest.Method.ToString(),
            };

            requestEntries.Add(requestData);
        }
    }

    public static void StoreApiResponseData(string requestId, HttpResponseMessage apiResponse, long responseTime)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            var responseData = new RequestEntry
            {
                Origem = "API Externa Response",
                Path = apiResponse.RequestMessage.RequestUri.ToString(),
                StatusCode = (int)apiResponse.StatusCode,
                IsSuccess = apiResponse.IsSuccessStatusCode,
                ResponseTime = responseTime
                // Adicione outras propriedades relevantes da resposta da API externa
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
                Origem = "API Response",
                Path = response.HttpContext.Request.Path,
                StatusCode = (int?)response.StatusCode,
                ResponseTime = responseTime
                // Adicione outras propriedades relevantes da resposta da API
            };

            requestEntries.Add(responseData);
        }
    }

    public static List<RequestEntry> GetRequestDataById(string requestId)
    {
        if (_requestData.TryGetValue(requestId, out var requestEntries))
        {
            return requestEntries.OrderBy(entry => entry.Origem).ToList();
        }

        return null;
    }


    public static List<List<RequestEntry>> GetAllRequestData()
    {
        return _requestData.Values.Select(requestEntries => requestEntries.OrderBy(entry => entry.Origem).ToList()).ToList();
    }

    public static void RemoveRequestData(string requestId)
    {
        _requestData.TryRemove(requestId, out _);
    }
}

public class RequestEntry
{
    public string Origem { get; set; }
    public string Path { get; set; }
    public int? StatusCode { get; set; }
    public string Method { get; set; }
    public bool IsSuccess { get; set; } = true;
    public long? ResponseTime { get; set; }
    // Adicione outras propriedades conforme necessário
}
