using System;
using System.IO;
using System.Collections.Concurrent;
using System.Linq;

namespace kAI_webAPI.Utils
{
    public static class Logger
    {
        private static string GetLogDirectory()
        {
            var projectDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(projectDir);
            return projectDir;
        }

        // Mapping sessionId -> temp file name
        private static readonly ConcurrentDictionary<string, string> SessionFiles = new();
        // Mapping sessionId -> last activity
        private static readonly ConcurrentDictionary<string, DateTime> SessionLastActivity = new();

        private static string CreateTempLogFile(string sessionId, string userAgent)
        {
            var logDir = GetLogDirectory();
            int idx = 1;
            string tempFile;
            do
            {
                tempFile = Path.Combine(logDir, $"temp{idx}.txt");
                idx++;
            } while (File.Exists(tempFile));

            var startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            File.WriteAllText(tempFile, $"Start: {startTime} | UserAgent: {userAgent}{Environment.NewLine}");
            SessionFiles[sessionId] = tempFile;
            SessionLastActivity[sessionId] = DateTime.Now;
            return tempFile;
        }

        private static string GetSessionLogFile(string sessionId, string userAgent)
        {
            if (!SessionFiles.TryGetValue(sessionId, out var file))
            {
                file = CreateTempLogFile(sessionId, userAgent);
            }
            SessionLastActivity[sessionId] = DateTime.Now;
            return file;
        }

        public static void LogInformation(string sessionId, string message, string userAgent, string? path = null, string? method = null, string? ip = null)
            => Log("Information", sessionId, message, userAgent, null, path, method, ip);

        public static void LogWarning(string sessionId, string message, string userAgent, Exception? ex = null, string? path = null, string? method = null, string? ip = null)
            => Log("Warning", sessionId, message, userAgent, ex, path, method, ip);

        public static void LogError(string sessionId, string message, string userAgent, Exception? ex = null, string? path = null, string? method = null, string? ip = null)
            => Log("Error", sessionId, message, userAgent, ex, path, method, ip);

        public static void LogCritical(string sessionId, string message, string userAgent, Exception? ex = null, string? path = null, string? method = null, string? ip = null)
            => Log("Critical", sessionId, message, userAgent, ex, path, method, ip);

        private static void Log(string level, string sessionId, string message, string userAgent, Exception? ex, string? path = null, string? method = null, string? ip = null)
        {
            var now = DateTime.Now;
            var filePath = GetSessionLogFile(sessionId, userAgent);

            var logMessage = $"[{now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            if (!string.IsNullOrEmpty(path))
                logMessage += $"{Environment.NewLine}Path: {path}";
            if (!string.IsNullOrEmpty(method))
                logMessage += $"{Environment.NewLine}Method: {method}";
            if (!string.IsNullOrEmpty(ip))
                logMessage += $"{Environment.NewLine}IP: {ip}";
            if (!string.IsNullOrEmpty(userAgent))
                logMessage += $"{Environment.NewLine}UserAgent: {userAgent}";
            if (ex != null)
                logMessage += $"{Environment.NewLine}{ex}";
            logMessage += Environment.NewLine;

            File.AppendAllText(filePath, logMessage);
            SessionLastActivity[sessionId] = now;
        }

        public static void EndSession(string sessionId)
        {
            if (SessionFiles.TryRemove(sessionId, out var tempFile) && File.Exists(tempFile))
            {
                var endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                File.AppendAllText(tempFile, $"End: {endTime}{Environment.NewLine}");

                var firstLine = File.ReadLines(tempFile).FirstOrDefault();
                string startTime = "unknown";
                if (firstLine != null && firstLine.StartsWith("Start: "))
                {
                    var parts = firstLine.Split('|')[0].Replace("Start: ", "").Trim();
                    startTime = parts.Replace(":", "-").Replace(" ", "_");
                }
                var logDir = GetLogDirectory();
                var newFileName = $"Logs_{startTime}.txt";
                var newFilePath = Path.Combine(logDir, newFileName);
                if (!string.Equals(tempFile, newFilePath, StringComparison.OrdinalIgnoreCase))
                {
                    File.Move(tempFile, newFilePath, true);
                }
            }
            SessionLastActivity.TryRemove(sessionId, out _);
        }

        // Hàm cho background service lấy các session hết hạn
        public static string[] GetExpiredSessions(TimeSpan timeout)
        {
            var now = DateTime.Now;
            return SessionLastActivity
                .Where(kv => (now - kv.Value) > timeout)
                .Select(kv => kv.Key)
                .ToArray();
        }
    }
}