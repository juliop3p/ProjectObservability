namespace Observability.Splunk.Models
{
    public class LogModel
    {
        public LogModel(string requestId, string application, string criticity, List<LogData> data)
        {
            RequestId = requestId;
            Application = application;
            Date = DateTime.UtcNow.ToString();
            Criticity = criticity;
            Server = Environment.MachineName;
            Data = data;
        }

        public string RequestId { get; set; }
        public string Application { get; set; }
        public string Date { get; set; }
        public string Criticity { get; set; }
        public string Server { get; set; }
        public List<LogData> Data { get; set; }
    }
}
