namespace WordParser.Core.Parser;

internal class WordParserMain : IWordParser
{
    private readonly WordParserProcessSettingsModel _wordParserProcessSettings;

    public string DetectorMessageError { get; private set; } = string.Empty;

    private HtmlWeb Web { get; set; } = new();
    private string InnerPageText { get; set; } = string.Empty;

    public WordParserMain(WordParserProcessSettingsModel wordParserProcessSettingsModel)
    {
        _wordParserProcessSettings = wordParserProcessSettingsModel;
    }

    public async Task<IList<WordModel>> Parse(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Url not specified.");

        if (!Regex.IsMatch(url, @"http(|s)://.*"))
            throw new ArgumentException($"The URL does not have a protocol.");

        var html = await Web.LoadFromWebAsync(url);

        if (html is null || string.IsNullOrWhiteSpace(html.Text))
            throw new InvalidOperationException("Html of the site is null or empty.");

        InnerPageText = html.DocumentNode.InnerText;
        var words = (await ApplyFilterAsync()).ToList();

        return words;
    }

    public async Task<IList<WordModel>> ReApplyFilterAsync()
    {
        _wordParserProcessSettings.SettingsIsUpdated = false;
        return !string.IsNullOrWhiteSpace(InnerPageText) ? (await ApplyFilterAsync()).ToList() : Array.Empty<WordModel>();
    }

    private async Task<IEnumerable<WordModel>> ApplyFilterAsync()
        => await Task.Run(() => ApplyFilter());

    private IEnumerable<WordModel> ApplyFilter()
    {
        var words = InnerPageText
            .Split(_wordParserProcessSettings.Separators, StringSplitOptions.None)
            .Select(x => Regex.Replace(x, @"(&nbsp.*?|&\#[0-9]+.*?)", string.Empty))
            .Where(x => !string.IsNullOrWhiteSpace(x));

        if (_wordParserProcessSettings.CheckIsLetter)
            words = words.Where(x => x.Any(c => char.IsLetter(c)));

        return new HashSet<WordModel>(words.Select(word => new WordModel
        {
            Word = word,
            Quantity = words.Count(w => string.Equals(word, w, StringComparison.CurrentCultureIgnoreCase))
        }))
        .Sort(_wordParserProcessSettings.SortMode)
        .ChangeCase(_wordParserProcessSettings.RegisterSettings) ?? Array.Empty<WordModel>();
    }
}