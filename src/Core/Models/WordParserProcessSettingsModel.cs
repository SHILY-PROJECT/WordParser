namespace WordParser.Core.Models;

internal class WordParserProcessSettingsModel
{
    private static readonly string[] _defaultSeparators = { " ", ",", ".", "!", "?", "\"", "; ", ":", "[", "]", "(", ")", "\n", "\r", "\t" };

    public WordSortType SortType { get; set; }
    public LetterСase LetterСase {  get; set; }
    public bool CheckIsLetter { get; set; }

    [JsonIgnore] public string[] Separators { get; set; } = _defaultSeparators;
    [JsonIgnore] public bool IsUpdated { get; set; }
}