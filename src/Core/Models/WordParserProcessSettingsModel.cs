namespace WordParser.Core.Models;

internal class WordParserProcessSettingsModel
{
    public WordSortType SortMode { get; set; }
    public LetterСase RegisterSettings {  get; set; }
    public bool CheckIsLetter { get; set; }

    [JsonIgnore]
    public string[] Separators { get; set; } = new[] { " ", ",", ".", "!", "?", "\"", "; ", ":", "[", "]", "(", ")", "\n", "\r", "\t" };
    
    [JsonIgnore]
    public bool SettingsIsUpdated { get; set; } = default;

    public WordParserProcessSettingsModel() { }

    public WordParserProcessSettingsModel(WordParserProcessSettingsModel settingsProcessingWords, bool settingsIsUpdated = false)
    {
        SortMode = settingsProcessingWords.SortMode;
        RegisterSettings = settingsProcessingWords.RegisterSettings;
        CheckIsLetter = settingsProcessingWords.CheckIsLetter;
        Separators = settingsProcessingWords.Separators;
        SettingsIsUpdated = settingsIsUpdated;
    }
}