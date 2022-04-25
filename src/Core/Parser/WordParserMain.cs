namespace WordParser.Core.Parser;

internal class WordParserMain
{
    private string _pageInnerText = string.Empty;

    internal string? DetectorMessageError { get; private set; }

    internal List<WordModel> Parse(string url, SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordModel>();

        try
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("URL не указан, либо указан некорректно...");

            if (!Regex.IsMatch(url, @"http(|s)://.*"))
                throw new Exception($"Отсутствует протокол. URL должен начинаться с \"https://{url}\", либо с \"http://{url}\".");

            var htmlPage = new HtmlWeb().Load(url);

            if (htmlPage is null || string.IsNullOrWhiteSpace(htmlPage.Text))
                throw new Exception("Не удалось спарсить сайт");

            _pageInnerText = htmlPage.DocumentNode.InnerText;
            resultWords = Parse(settingsProcessingWords);

            if (resultWords.Count == 0)
                throw new Exception($"Результат парсинга пуст...");
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}  |  URL: {url}";
            Logger.WriteAsync(DetectorMessageError, LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        return resultWords;
    }

    internal List<WordModel> ReParsing(SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordModel>();

        if (string.IsNullOrWhiteSpace(_pageInnerText)) return resultWords;

        try
        {
            resultWords = Parse(settingsProcessingWords);
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}";
            Logger.WriteAsync(DetectorMessageError, LogTypeEnum.Error).GetAwaiter().GetResult();
        }

        return resultWords;
    }

    private List<WordModel> Parse(SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordModel>();

        var words = _pageInnerText
            .Split(settingsProcessingWords.Separators, StringSplitOptions.None)
            .Select(x => Regex.Replace(x, @"(&nbsp.*?|&\#[0-9]+.*?)", string.Empty))
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        if (settingsProcessingWords.CheckIsLetter)
            words = words.Where(x => x.Any(c => char.IsLetter(c))).ToList();

        words.ForEach(word =>
        {
            if (!resultWords.Any(uWord => string.Equals(uWord.Word, word, StringComparison.CurrentCultureIgnoreCase)))
                resultWords.Add(new WordModel(word, words.Count(x => string.Equals(x, word, StringComparison.CurrentCultureIgnoreCase))));
        });

        resultWords = resultWords?.Sort(settingsProcessingWords.SortMode) as List<WordModel>;
        resultWords = resultWords?.ChangeCase(settingsProcessingWords.RegisterSettings) as List<WordModel>;

        return resultWords ?? new List<WordModel>();
    }

}