namespace WordParser.Core.Toolkit.LoggerTool;

internal class Logger
{
    private static readonly FileInfo _logFile = new(Path.Combine("logs", $"log - {DateTime.Now:yyyy-MM-dd}.log"));

    private static string CurrentDate { get => $"{DateTime.Now:yyyy-MM-dd   HH-mm-ss}"; }

    private static FileInfo LogFile
    {
        get
        {
            if (_logFile.Directory != null && !_logFile.Directory.Exists)
                _logFile.Directory.Create();
            return _logFile;
        }
    }

    public static void Write(string message, LogType typeLog) =>
        File.AppendAllLines(LogFile.FullName, new[] { FormatMessage(typeLog, message) }, Encoding.UTF8);

    public static async Task WriteAsync(string message, LogType typeLog) =>
        await File.AppendAllLinesAsync(LogFile.FullName, new[] { FormatMessage(typeLog, message) }, Encoding.UTF8);

    private static string FormatMessage(LogType typeLog, string message) => new Dictionary<LogType, string>
    {
        [LogType.Info] = $"[{CurrentDate}]      |INFO|      {message}",
        [LogType.Warning] = $"[{CurrentDate}]      |WARNING|   {message}",
        [LogType.Error] = $"[{CurrentDate}]      |ERROR|     {message}"
    }
    [typeLog];
}