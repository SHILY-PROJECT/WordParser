namespace WordParser.Core.Models;

internal class SettingsProcessingWordsModel
{
    public SortingWordsSettingsEnum SortingMode { get; set; }
    public LetterСaseEnum RegisterSettings {  get; set; }
    public bool CheckIsLetter { get; set; }

    [JsonIgnore]
    public string[] Separators { get; set; } = new[] { " ", ",", ".", "!", "?", "\"", "; ", ":", "[", "]", "(", ")", "\n", "\r", "\t" };
    
    [JsonIgnore]
    public bool SettingsIsUpdated { get; set; } = default;

    public SettingsProcessingWordsModel() { }

    public SettingsProcessingWordsModel(SettingsProcessingWordsModel settingsProcessingWords, bool settingsIsUpdated = false)
    {
        SortingMode = settingsProcessingWords.SortingMode;
        RegisterSettings = settingsProcessingWords.RegisterSettings;
        CheckIsLetter = settingsProcessingWords.CheckIsLetter;
        Separators = settingsProcessingWords.Separators;
        SettingsIsUpdated = settingsIsUpdated;
    }
}