using Observability.Splunk.Models;
using System.Collections.Concurrent;

namespace Observability.Splunk
{
    public static class Logger
    {
        private static readonly ConcurrentDictionary<string, List<LogData>> requestLogs = new ConcurrentDictionary<string, List<LogData>>();

        public static void LogInfo(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                requestId,
                criticity: "Informação",
                data: data
            );

            requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static void LogWarning(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                requestId,
                criticity: "Aviso",
                data: data
            );

            requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static void LogError(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                requestId,
                criticity: "Erro",
                data: data
            );

            requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static void SaveLog()
        {
            var splunkClient = new SplunkClient();
            string requestId = RequestIdContext.Current;

            if (requestLogs.TryRemove(requestId, out var logs))
            {

                var log = new LogModel(
                    requestId,
                    application: AppDomain.CurrentDomain.FriendlyName,
                    criticity: GetLogCriticity(logs),
                    data: logs
                );

                splunkClient.SendLogToSplunk(log);
            }
        }

        private static LogData CreateLogData(string requestId, string criticity, object data)
        {
            // Aqui você pode personalizar a criação do objeto LogData conforme suas necessidades
            return new LogData(requestId, criticity, data);
        }

        private static string GetLogKey(string requestId)
        {
            return $"{AppDomain.CurrentDomain.FriendlyName}_{requestId}";
        }

        private static string GetLogCriticity(List<LogData> logsData)
        {
            if (logsData.Any(log => log.Criticity == "Erro"))
            {
                return "Erro";
            }
            else if (logsData.Any(log => log.Criticity == "Aviso"))
            {
                return "Aviso";
            }
            else
            {
                return "Informação";
            }
        }
    }
}
