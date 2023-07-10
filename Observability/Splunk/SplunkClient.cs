namespace Observability.Splunk
{
    public class SplunkClient
    {
        private readonly HttpClient _httpClient;

        public SplunkClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendLogToSplunk(string logMessage)
        {
            var requestUri = "http://localhost:8080";
            var requestContent = new StringContent(logMessage);

            try
            {
                var response = await _httpClient.PostAsync(requestUri, requestContent);
                response.EnsureSuccessStatusCode();

                Console.WriteLine("Log enviado com sucesso para o Splunk!");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao enviar log para o Splunk: {ex.Message}");
            }
        }
    }
}
