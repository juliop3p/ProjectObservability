namespace Observability.Splunk.Models
{
    public class LogData
    {
        public LogData(string requestId, string criticity, object data)
        {
            RequestId = requestId;
            Date = DateTime.UtcNow.ToString();
            Criticity = criticity;
            Data = data;
        }

        public string RequestId { get; set; }
        public string Date { get; set; }
        public string Criticity { get; set; }
        public object Data { get; set; }
    }
}
