using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Observability.Splunk
{
    public class SplunkClient
    {
        private readonly HttpClient _httpClient;

        public SplunkClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendLogToSplunk(object logData)
        {
            try
            {
                // Salva logs localy for now
                string LogFilePath = "logs.txt";

                using (StreamWriter writer = File.AppendText(LogFilePath))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(logData));
                }

                Console.WriteLine("Log enviado com sucesso para o Splunk!");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao enviar log para o Splunk: {ex.Message}");
            }
        }
    }
}
