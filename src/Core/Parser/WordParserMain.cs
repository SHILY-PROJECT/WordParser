namespace WordParser.Core.Parser;

internal class WordParserMain : IWordParser
{
    private readonly WordParserProcessSettingsModel _wordParserProcessSettings;

    private HtmlWeb Web { get; set; } = new();
    private HtmlAgilityPack.HtmlDocument HtmlDoc { get; set; } = new();
    private string InnerPageText { get => HtmlDoc.DocumentNode.InnerText; }

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

        HtmlDoc = await Web.LoadFromWebAsync(url);

        if (string.IsNullOrWhiteSpace(HtmlDoc.Text))
            throw new InvalidOperationException("Html of the site is null or empty.");

        var words = (await ApplyFilterAsync()).ToList();

        return words;
    }

    public async Task<IList<WordModel>> ReApplyFilterAsync()
    {
        _wordParserProcessSettings.IsUpdated = false;
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
        .Sort(_wordParserProcessSettings.SortType)
        .ChangeCase(_wordParserProcessSettings.LetterСase) ?? Array.Empty<WordModel>();
    }
}