namespace WordParser.Core.Interfaces;

internal interface IWordParserSettingsHandler
{
    event EventHandler<WordParseSettingsChangesEventArgs>? SettingsChanged;
    WordParserProcessSettingsModel CurrentSettings { get; }
    void SaveSettings();
    void ChangeSettingsIfNeeded(WordParserProcessSettingsModel updatedSettings);
}