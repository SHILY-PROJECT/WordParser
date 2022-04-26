namespace WordParser.Core.Models;

internal class WordParserProcessSettingsModel
{
    private static readonly string[] _defaultSeparators = { " ", ",", ".", "!", "?", "\"", "; ", ":", "[", "]", "(", ")", "\n", "\r", "\t" };

    public WordSortType SortType { get; set; }
    public LetterСase LetterСase {  get; set; }
    public bool CheckIsLetter { get; set; }

    [JsonIgnore] public string[] Separators { get; set; } = _defaultSeparators;
    [JsonIgnore] public bool IsUpdated { get; set; }

    public void ChangeSettingsIfNeeded(WordParserProcessSettingsModel settings)
    {
        foreach (var prop in typeof(WordParserProcessSettingsModel).GetProperties())
        {
            object? value;

            if (prop.Name.Equals(nameof(IsUpdated)) || (value = prop.GetValue(settings)) is null || value.Equals(prop.GetValue(this))) continue;
            
            prop.SetValue(this, value);
            IsUpdated = true;
        }
    }
}