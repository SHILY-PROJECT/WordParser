namespace WordParser.Core.Toolkit;

internal class Logger
{
    private static readonly FileInfo _log = new(Path.Combine("logs", $"log - {DateTime.Now:yyyy-MM-dd}.log"));

    private static string CurrentDate { get => $"{DateTime.Now:yyyy-MM-dd   HH-mm-ss}"; }

    public static async void Write(string message, LogTypeEnum typeLog)
    {
        if (_log == null || _log.Directory == null) return;
        else if (!_log.Directory.Exists) _log.Directory.Create();

        var formatting = new Dictionary<LogTypeEnum, string>
        {
            [LogTypeEnum.Info] = $"[{CurrentDate}]      |INFO|      {message}",
            [LogTypeEnum.Warning] = $"[{CurrentDate}]      |WARNING|   {message}",
            [LogTypeEnum.Error] = $"[{CurrentDate}]      |ERROR|     {message}"
        };

        await File.AppendAllLinesAsync(_log.FullName, new[] { formatting[typeLog] }, Encoding.UTF8);
    }
}