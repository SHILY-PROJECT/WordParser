namespace WordParser.Core.Parser;

internal class WordParserSettingsHandler : IWordParserSettingsHandler
{
    private readonly FileInfo _settingsFile = new("word_parser_process_settings.ini");
    private WordParserProcessSettingsModel? _currentSettings;

    public event EventHandler<WordParseSettingsChangesEventArgs>? SettingsChanged;

    public WordParserProcessSettingsModel CurrentSettings { get => _currentSettings ??= GetSettings(); }

    public void SaveSettings()
    {
        var settingsContent = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsFile.FullName, settingsContent, Encoding.UTF8);
    }

    public void ChangeSettingsIfNeeded(WordParserProcessSettingsModel updatedSettings)
    {
        var isChanged = default(bool);

        foreach (var prop in typeof(WordParserProcessSettingsModel).GetProperties())
        {
            if (prop.GetValue(updatedSettings) is object value && !value.Equals(prop.GetValue(CurrentSettings)))
            {
                prop.SetValue(CurrentSettings, value);
                isChanged = true;
            }
        }

        if (isChanged) OnSettingsChanged();
    }

    protected virtual void OnSettingsChanged()
        => SettingsChanged?.Invoke(this, new(ParserMode.ReApplyFilter));

    private WordParserProcessSettingsModel GetSettings()
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
        catch { }

        return wordParserProcessSettings ?? new();
    }
}