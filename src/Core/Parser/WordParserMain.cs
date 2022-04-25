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
        var words = new List<WordModel>();

        try
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL не указан, либо указан некорректно...");

            if (!Regex.IsMatch(url, @"http(|s)://.*"))
                throw new ArgumentException($"Отсутствует протокол. URL должен начинаться с \"https://{url}\", либо с \"http://{url}\".");

            var html = await Web.LoadFromWebAsync(url);

            if (html is null || string.IsNullOrWhiteSpace(html.Text))
                throw new InvalidOperationException("Не удалось спарсить сайт");

            InnerPageText = html.DocumentNode.InnerText;
            words = (await ApplyFilterAsync()).ToList();

            if (!words.Any())
                throw new Exception($"Результат парсинга пуст...");
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}  |  URL: {url}";
            Logger.WriteAsync(DetectorMessageError, LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        return words;
    }

    public async Task<IList<WordModel>> ReApplyFilterAsync()
    {
        var words = new List<WordModel>();

        if (string.IsNullOrWhiteSpace(InnerPageText)) return words;

        try
        {
            words = (await ApplyFilterAsync()).ToList();
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}";
            Logger.WriteAsync(DetectorMessageError, LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        _wordParserProcessSettings.SettingsIsUpdated = false;
        return words;
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