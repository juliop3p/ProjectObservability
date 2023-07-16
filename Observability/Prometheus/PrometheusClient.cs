using Prometheus;

namespace Observability.Prometheus
{
    public class PrometheusClient
    {
        private static readonly Counter RequestsCounter = Metrics.CreateCounter("requestsEndToEnd", "Total number of requests received", new CounterConfiguration
        {
            LabelNames= new[] { "ApiPath", "Method", "Result", "ApiExternal", "ApiExternalResult"}
        });

        public void SendLogToPrometheus(List<RequestEntry> requests)
        {
            var apiRequest = requests.Find(x => x.RequestType.Equals("API Request"));
            var apiResponse = requests.Find(x => x.RequestType.Equals("API Response"));
            var apiExternalRequest = requests.Find(x => x.RequestType.Equals("API External Request"));
            var apiExternalResponse = requests.Find(x => x.RequestType.Equals("API Externa Response"));
            RequestsCounter.WithLabels(apiRequest.Path, apiRequest.Method, apiResponse.StatusCode.ToString(), apiExternalResponse?.Path ?? "", apiExternalResponse?.StatusCode?.ToString() ?? "").Inc();
        }
    }
}
