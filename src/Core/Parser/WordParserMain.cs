namespace WordParser.Core.Parser;

internal class WordParserMain
{
    private string _pageInnerText = string.Empty;

    internal string? DetectorMessageError { get; private set; }

    internal List<WordItemModel> Parse(string url, SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordItemModel>();

        try
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("URL не указан, либо указан некорректно...");

            if (!Regex.IsMatch(url, @"http(|s)://.*"))
                throw new Exception($"Отсутствует протокол. URL должен начинаться с \"https://{url}\", либо с \"http://{url}\".");

            var htmlPage = new HtmlWeb().Load(url);

            if (htmlPage == null || string.IsNullOrWhiteSpace(htmlPage.Text))
                throw new Exception("Не удалось спарсить сайт");

            _pageInnerText = htmlPage.DocumentNode.InnerText;
            resultWords = Parse(settingsProcessingWords);

            if (resultWords.Count == 0)
                throw new Exception($"Результат парсинга пуст...");
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}  |  URL: {url}";
            Logger.Write(DetectorMessageError, LogTypeEnum.Error);
        }

        return resultWords;
    }

    internal List<WordItemModel> ReParsing(SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordItemModel>();

        if (string.IsNullOrWhiteSpace(_pageInnerText)) return resultWords;

        try
        {
            resultWords = Parse(settingsProcessingWords);
        }
        catch (Exception ex)
        {
            DetectorMessageError = $"MESSAGE ERROR: {ex.Message}";
            Logger.Write(DetectorMessageError, LogTypeEnum.Error);
        }

        return resultWords;
    }

    private List<WordItemModel> Parse(SettingsProcessingWordsModel settingsProcessingWords)
    {
        var resultWords = new List<WordItemModel>();

        var words = _pageInnerText
            .Split(settingsProcessingWords.Separators, StringSplitOptions.None)
            .Select(x => Regex.Replace(x, @"(&nbsp.*?|&\#[0-9]+.*?)", string.Empty))
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        if (settingsProcessingWords.CheckIsLetter)
            words = words.Where(x => x.Any(c => char.IsLetter(c))).ToList();

        words.ForEach(word =>
        {
            if (!resultWords.Any(uWord => string.Equals(uWord.Word, word, StringComparison.CurrentCultureIgnoreCase)))
                resultWords.Add(new WordItemModel(word, words.Count(x => string.Equals(x, word, StringComparison.CurrentCultureIgnoreCase))));
        });

        resultWords = this.ProcessSortingWords(resultWords, settingsProcessingWords.SortingMode) as List<WordItemModel>;
        resultWords = this.ProcessRegisterWords(resultWords, settingsProcessingWords.RegisterSettings) as List<WordItemModel>;

        return resultWords;
    }

    private IEnumerable<WordItemModel> ProcessSortingWords(IEnumerable<WordItemModel> words, SortingWordsSettingsEnum sortingWords) => sortingWords switch
    {
        SortingWordsSettingsEnum.SortByAlphabet => words.OrderBy(x => x.Word),
        SortingWordsSettingsEnum.SortByUniquenessFromMin => words.OrderBy(x => x.Quantity),
        SortingWordsSettingsEnum.SortByUniquenessFromMax => words.OrderByDescending(x => x.Quantity),
        _ or SortingWordsSettingsEnum.NoSorting => words,
    };

    private IEnumerable<WordItemModel> ProcessRegisterWords(IEnumerable<WordItemModel> words, LetterСaseEnum letterСase) => letterСase switch
    {
        LetterСaseEnum.FirstLetterInUpper => words.Select(x => new WordItemModel(x.Word != null ? new string(x.Word.Select((c, index) => index == 0 ? char.ToUpper(c) : char.ToLower(c)).ToArray()) : string.Empty, x.Quantity)),
        LetterСaseEnum.AllLetterInUpper => words.Select(x => new WordItemModel(x.Word != null ? x.Word.ToUpper() : string.Empty, x.Quantity)),
        LetterСaseEnum.AllLetterInLower => words.Select(x => new WordItemModel(x.Word != null ? x.Word.ToLower() : string.Empty, x.Quantity)),
        _ or LetterСaseEnum.Default => words
    };
}