namespace WordParser.Core.Parser.Extensions;

internal static class WordSorter
{
    public static IEnumerable<WordModel> Sort(this IEnumerable<WordModel> words, WordSortType wordSortType) => wordSortType switch
    {
        WordSortType.SortByAlphabet             => words.OrderBy(x => x.Word),
        WordSortType.SortByUniquenessFromMin    => words.OrderBy(x => x.Quantity),
        WordSortType.SortByUniquenessFromMax    => words.OrderByDescending(x => x.Quantity),
        WordSortType.NoSorting or _             => words,
    };
}