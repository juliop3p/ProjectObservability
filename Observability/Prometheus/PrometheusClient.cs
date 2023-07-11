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
            var apiRequest = requests.Find(x => x.Origem.Equals("API Request"));
            var apiResponse = requests.Find(x => x.Origem.Equals("API Response"));
            RequestsCounter.WithLabels(apiRequest.Path, apiRequest.Method, apiResponse.StatusCode.ToString(), "", "").Inc();
        }
    }
}
