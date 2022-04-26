namespace WordParser.Core.Parser;

internal class WordParserSettingsHandler
{
    private static readonly FileInfo _settingsFile = new("word_parser_process_settings.ini");

    public static void SaveSettings(WordParserProcessSettingsModel wordParserProcessSettings)
    {
        var settingsContent = JsonSerializer.Serialize(wordParserProcessSettings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsFile.FullName, settingsContent, Encoding.UTF8);
    }

    public static WordParserProcessSettingsModel GetSettings()
    {
        var wordParserProcessSettings = new WordParserProcessSettingsModel();

        try
        {
            if (_settingsFile.Exists)
            {
                var settingsContent = File.ReadAllText(_settingsFile.FullName, Encoding.UTF8);
                wordParserProcessSettings = JsonSerializer.Deserialize<WordParserProcessSettingsModel>(settingsContent);
            }
        }
        catch (Exception ex)
        {
            Logger.WriteAsync($"Не удалось загрузить настройки парсера  |  MESSAGE ERROR: {ex.Message}", LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        return wordParserProcessSettings ?? new();
    }
}