using Observability.Splunk.Models;
using System.Collections.Concurrent;

namespace Observability.Splunk
{
    public static class Logger
    {
        private static readonly ConcurrentDictionary<string, List<LogData>> _requestLogs = new ConcurrentDictionary<string, List<LogData>>();

        public static void LogInfo(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                criticity: "Informação",
                data: data
            );

            _requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static void LogWarning(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                criticity: "Aviso",
                data: data
            );

            _requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static void LogError(object data)
        {
            string requestId = RequestIdContext.Current;

            var log = new LogData(
                criticity: "Erro",
                data: data
            );

            _requestLogs.AddOrUpdate(
                requestId,
                new List<LogData> { log },
                (_, logs) => { logs.Add(log); return logs; }
            );
        }

        public static List<LogData> GetLogsById(string requestId)
        {
            if (_requestLogs.TryRemove(requestId, out var logs))
            {
                return logs ;
            }

            return new List<LogData>();
        }

        public static void RemoveLogsById(string requestId)
        {
            _requestLogs.TryRemove(requestId, out var _);
        }

        public static string GetLogCriticity(List<LogData> logsData)
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
