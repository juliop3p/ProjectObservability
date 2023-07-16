namespace Observability.Splunk.Models
{
    public class LogData
    {
        public LogData(string criticity, object data)
        {
            Date = DateTime.UtcNow.ToString();
            Criticity = criticity;
            Data = data;
        }

        public string Date { get; set; }
        public string Criticity { get; set; }
        public object Data { get; set; }
    }
}
