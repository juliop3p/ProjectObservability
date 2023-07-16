using Microsoft.AspNetCore.Http;
using Observability.Prometheus;
using Observability.Splunk;
using Observability.Splunk.Models;
using System.Diagnostics;
using System.Text;

namespace Observability.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Captura a requisição
            string requestId = RequestIdContext.Current;

            RequestDataStorage.StoreRequestData(requestId, context.Request);

            // Captura a resposta
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            // Processa a resposta
            string responseBodyText = await ReadResponseBodyAsync(context.Response);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            RequestDataStorage.StoreResponseData(requestId, context.Response, elapsedMilliseconds);
             
            //    // Aqui você pode fazer o que desejar com as informações capturadas
            //    // Por exemplo, enviar para o Splunk

            // Restaura o corpo original da resposta
            await responseBody.CopyToAsync(originalBodyStream);

            // Executar o método ao final da solicitação
            await ProcessRequestAsync(context);
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            // Verifique se a requisição possui um corpo antes de lê-lo
            if (request.ContentLength > 0)
            {
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                string body = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
                return body;
            }

            return string.Empty;
        }

        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string body = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }

        //private async Task ProcessRequestAsync(HttpContext context)
        //{
        //    // Coloque o código que deseja executar ao final da solicitação aqui
        //    string requestId = RequestIdContext.Current;

        //    // SEND LOGS TO SPLUNK
        //    var splunk = new SplunkClient();
        //    var logsCustom = Logger.GetLogsById(requestId);

        //    var logResult = new LogModel(
        //            requestId,
        //            application: AppDomain.CurrentDomain.FriendlyName,
        //            criticity: Logger.GetLogCriticity(logsCustom),
        //            data: logsCustom
        //        );

        //    var requestData = RequestDataStorage.GetRequestDataById(requestId);

        //    logResult.Request = requestData;

        //    splunk.SendLogToSplunk(logResult);

        //    // limpa os logs
        //    RequestDataStorage.RemoveRequestData(requestId);
        //    Logger.RemoveLogsById(requestId);


        //    // SEND LOGS TO PROMETHEUS
        //    var prometheus = new PrometheusClient();
        //    prometheus.SendLogToPrometheus(requestData);
        //}

        private async Task ProcessRequestAsync(HttpContext context)
        {
            string requestId = RequestIdContext.Current;

            var logsCustom = Logger.GetLogsById(requestId);
            var requestData = RequestDataStorage.GetRequestDataById(requestId);

            SendLogsToSplunk(requestId, logsCustom, requestData);
            ClearRequestDataAndLogs(requestId);
            SendLogsToPrometheus(requestData);
        }

        private void SendLogsToSplunk(string requestId, List<LogData> logsCustom, List<RequestEntry> requestData)
        {
            var splunk = new SplunkClient();
            var logResult = new LogModel(
                requestId,
                application: AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                criticity: Logger.GetLogCriticity(logsCustom),
                data: logsCustom
            );
            logResult.Request = requestData;
            splunk.SendLogToSplunk(logResult);
        }

        private void ClearRequestDataAndLogs(string requestId)
        {
            RequestDataStorage.RemoveRequestData(requestId);
            Logger.RemoveLogsById(requestId);
        }

        private void SendLogsToPrometheus(List<RequestEntry> requestData)
        {
            var prometheus = new PrometheusClient();
            prometheus.SendLogToPrometheus(requestData);
        }

    }
}
