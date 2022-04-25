namespace WordParser.Core.Models;

internal class SettingsProcessingWordsModel
{
    public WordSortType SortMode { get; set; }
    public LetterСaseEnum RegisterSettings {  get; set; }
    public bool CheckIsLetter { get; set; }

    [JsonIgnore]
    public string[] Separators { get; set; } = new[] { " ", ",", ".", "!", "?", "\"", "; ", ":", "[", "]", "(", ")", "\n", "\r", "\t" };
    
    [JsonIgnore]
    public bool SettingsIsUpdated { get; set; } = default;

    public SettingsProcessingWordsModel() { }

    public SettingsProcessingWordsModel(SettingsProcessingWordsModel settingsProcessingWords, bool settingsIsUpdated = false)
    {
        SortMode = settingsProcessingWords.SortMode;
        RegisterSettings = settingsProcessingWords.RegisterSettings;
        CheckIsLetter = settingsProcessingWords.CheckIsLetter;
        Separators = settingsProcessingWords.Separators;
        SettingsIsUpdated = settingsIsUpdated;
    }
}