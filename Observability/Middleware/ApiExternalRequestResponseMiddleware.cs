using System.Diagnostics;

public class ApiExternalRequestResponseMiddleware : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Capturar o RequestID antes de enviar para o próximo handler
        string requestId = RequestIdContext.Current;

        // Capturar a requisição antes de enviar para o próximo handler
        string requestUri = request.RequestUri.ToString();
        
        string requestBody = null;
        RequestDataStorage.StoreApiRequestData(requestId, request);


        if (request != null && request.Content != null)
        {
            requestBody = await request.Content.ReadAsStringAsync();
        }

        // Enviar a requisição para o próximo handler e obter a resposta
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // Capturar a resposta após o processamento
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        RequestDataStorage.StoreApiResponseData(requestId, response, elapsedMilliseconds);

        string responseBody = null;

        if (response != null && response.Content != null)
        {
            responseBody = await response.Content.ReadAsStringAsync();
        }

        return response;
    }
}
