namespace WordParser.Core.Parser;

internal class WordParserSettingsHandler
{
    private static readonly FileInfo _settingsProcessingWordsFile = new("settings_processing_words.ini");

    public static void SaveSettings(WordParserProcessSettingsModel settingsProcessingWords)
    {
        var settingsContent = JsonSerializer.Serialize(settingsProcessingWords, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsProcessingWordsFile.FullName, settingsContent, Encoding.UTF8);
    }

    public static WordParserProcessSettingsModel GetSettings()
    {
        var settingsProcessingWords = new WordParserProcessSettingsModel();

        try
        {
            if (_settingsProcessingWordsFile.Exists)
            {
                var settingsContent = File.ReadAllText(_settingsProcessingWordsFile.FullName, Encoding.UTF8);
                settingsProcessingWords = JsonSerializer.Deserialize<WordParserProcessSettingsModel>(settingsContent) ?? new();
            }
        }
        catch (Exception ex)
        {
            Logger.WriteAsync($"Не удалось загрузить настройки парсера  |  MESSAGE ERROR: {ex.Message}", LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        return settingsProcessingWords;
    }
}