namespace WordParser.Core.Parser;

internal class WordParserMain : IWordParser
{
    private readonly IWordParserSettingsHandler _wordParserSettingsHandler;

    private HtmlWeb Web { get; set; } = new();
    private HtmlAgilityPack.HtmlDocument HtmlDoc { get; set; } = new();
    private string InnerPageText { get => HtmlDoc.DocumentNode.InnerText; }

    public WordParserMain(IWordParserSettingsHandler wordParserSettingsHandler)
    {
        _wordParserSettingsHandler = wordParserSettingsHandler;
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
        => !string.IsNullOrWhiteSpace(InnerPageText) ? (await ApplyFilterAsync()).ToList() : Array.Empty<WordModel>();

    private async Task<IEnumerable<WordModel>> ApplyFilterAsync()
        => await Task.Run(() => ApplyFilter());

    private IEnumerable<WordModel> ApplyFilter()
    {
        var cfg = _wordParserSettingsHandler.CurrentSettings;

        var words = InnerPageText
            .Split(cfg.Separators, StringSplitOptions.None)
            .Select(x => Regex.Replace(x, @"(&nbsp.*?|&\#[0-9]+.*?)", string.Empty))
            .Where(x => !string.IsNullOrWhiteSpace(x));

        if (cfg.CheckIsLetter)
            words = words.Where(x => x.Any(c => char.IsLetter(c)));

        return new HashSet<WordModel>(words.Select(word => new WordModel
        {
            Word = word,
            Quantity = words.Count(w => string.Equals(word, w, StringComparison.CurrentCultureIgnoreCase))
        }),
        new WordEqualityComparer()).Sort(cfg.SortType).ChangeCase(cfg.LetterCase) ?? Array.Empty<WordModel>();
    }
}