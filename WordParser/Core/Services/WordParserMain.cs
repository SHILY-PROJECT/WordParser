using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WordParser.Core.Enums.Logger;
using WordParser.Core.Enums.Services;
using WordParser.Core.Models;
using WordParser.Core.Toolkit;

namespace WordParser.Core.Services
{
    internal class WordParserMain
    {
        /// <summary>
        ///Спаршеная страница.
        /// </summary>
        private string _pageInnerText = string.Empty;

        /// <summary>
        /// Информация об ошибке (если удалось определить).
        /// </summary>
        internal string? DetectorMessageError { get; private set; }

        /// <summary>
        /// Старт парсинга.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal List<WordItemModel> StartParsing(string url, SettingsProcessingWordsModel settingsProcessingWords)
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

        /// <summary>
        /// Парсинг данных в соответствии с новыми настройками.
        /// </summary>
        /// <param name="settingsProcessingWords"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Парсинг данных.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
            
            resultWords = ProcessSortingWords(resultWords, settingsProcessingWords.SortingMode);
            resultWords = ProcessRegisterWords(resultWords, settingsProcessingWords.RegisterSettings);

            return resultWords;
        }

        /// <summary>
        /// Сортировка результата.
        /// </summary>
        /// <param name="sortingWords"></param>
        /// <returns></returns>
        private List<WordItemModel> ProcessSortingWords(List<WordItemModel> words, SortingWordsSettingsEnum sortingWords) => sortingWords switch
        {
            SortingWordsSettingsEnum.NoSorting => words,
            SortingWordsSettingsEnum.SortByAlphabet => words.OrderBy(x => x.Word).ToList(),
            SortingWordsSettingsEnum.SortByUniquenessFromMin => words.OrderBy(x => x.WordUniqueness).ToList(),
            SortingWordsSettingsEnum.SortByUniquenessFromMax => words.OrderByDescending(x => x.WordUniqueness).ToList(),
            _ => throw new NotImplementedException(),
        };

        /// <summary>
        /// Обработка регистра результата.
        /// </summary>
        /// <param name="words"></param>
        /// <param name="registerWords"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<WordItemModel> ProcessRegisterWords(List<WordItemModel> words, RegisterWordsSettingsEnum registerWords) => registerWords switch
        {
            RegisterWordsSettingsEnum.OriginalView => words,
            RegisterWordsSettingsEnum.FirstSymbolInUpperRegister => words.Select(x => new WordItemModel(x.Word != null ? new string(x.Word.Select((c, index) => index == 0 ? char.ToUpper(c) : char.ToLower(c)).ToArray()) : string.Empty, x.WordUniqueness)).ToList(),
            RegisterWordsSettingsEnum.AllSymbolInUpperRegister => words.Select(x => new WordItemModel(x.Word != null ? x.Word.ToUpper() : string.Empty, x.WordUniqueness)).ToList(),
            RegisterWordsSettingsEnum.AllSymbolInLowerRegister => words.Select(x => new WordItemModel(x.Word != null ? x.Word.ToLower() : string.Empty, x.WordUniqueness)).ToList(),
            _ => throw new NotImplementedException(),
        };
    }
}
