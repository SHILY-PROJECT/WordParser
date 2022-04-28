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

    public static void Write(string message, LogTypeEnum typeLog) =>
        File.AppendAllLines(LogFile.FullName, new[] { FormatMessage(typeLog, message) }, Encoding.UTF8);

    public static async Task WriteAsync(string message, LogTypeEnum typeLog) =>
        await File.AppendAllLinesAsync(LogFile.FullName, new[] { FormatMessage(typeLog, message) }, Encoding.UTF8);

    private static string FormatMessage(LogTypeEnum typeLog, string message) => new Dictionary<LogTypeEnum, string>
    {
        [LogTypeEnum.Info] = $"[{CurrentDate}]      |INFO|      {message}",
        [LogTypeEnum.Warning] = $"[{CurrentDate}]      |WARNING|   {message}",
        [LogTypeEnum.Error] = $"[{CurrentDate}]      |ERROR|     {message}"
    }
    [typeLog];
}