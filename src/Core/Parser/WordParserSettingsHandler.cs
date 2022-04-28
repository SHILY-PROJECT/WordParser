namespace WordParser.Core.Parser;

internal class WordParserSettingsHandler : IWordParserSettingsHandler
{
    private readonly FileInfo _settingsFile = new("word_parser_process_settings.ini");
    private WordParserProcessSettingsModel? _currentSettings;

    public WordParserProcessSettingsModel CurrentSettings { get => _currentSettings ??= Load(); }

    public event EventHandler<WordParseSettingsChangesEventArgs>? SettingsChanged;

    public void SaveSettings()
    {
        var cfgContents = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_settingsFile.FullName, cfgContents, Encoding.UTF8);
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

    protected virtual void OnSettingsChanged() =>
        SettingsChanged?.Invoke(this, new(ParserMode.ReApplyFilter));

    private WordParserProcessSettingsModel Load()
    {
        WordParserProcessSettingsModel? cfg = null;

        try
        {
            if (_settingsFile.Exists)
            {
                var cfgContents = File.ReadAllText(_settingsFile.FullName, Encoding.UTF8);
                cfg = JsonSerializer.Deserialize<WordParserProcessSettingsModel>(cfgContents);
            }
        }
        catch (Exception ex)
        {
            Logger.Write(ex.Message, LogType.Error);
        }

        return cfg ?? new();
    }
}