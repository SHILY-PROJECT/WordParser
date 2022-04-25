namespace WordParser.Core.Parser.Extensions;

internal static class KeeperOfResult
{
    public static async Task SaveToFileAsync(this IEnumerable<WordModel> words, ResultFileType resultFileType)
    {
        var format = resultFileType switch
        {
            ResultFileType.Csv => "{0},{1}",
            ResultFileType.Txt => "{0} - {1}",
            _ => throw new ArgumentException($"'{nameof(ResultFileType)}:{resultFileType}' - invalid argument.")
        };
        var file = GetFileNameToSaveResult(resultFileType);

        await File.WriteAllLinesAsync(file.FullName, words.Select(x => string.Format(format, x.Word, x.Quantity)), Encoding.UTF8);
        OpenFileInExplorer(file);
    }

    private static FileInfo GetFileNameToSaveResult(ResultFileType rsultFileType)
    {
        var extension = rsultFileType switch
        {
            ResultFileType.Csv => ".csv",
            ResultFileType.Txt or _ => ".txt",
        };
        var file = new FileInfo(Path.Combine("result", $"result   {DateTime.Now:yyyy-MM-dd   HH-mm-ss---fffffff}{extension}"));

        if (file.Directory != null && !file.Directory.Exists) file.Directory.Create();

        return file;
    }

    private static void OpenFileInExplorer(FileInfo file)
        => Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, {file.FullName}" });
}