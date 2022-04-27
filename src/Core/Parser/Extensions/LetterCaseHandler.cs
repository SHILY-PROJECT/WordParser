namespace WordParser.Core.Parser.Extensions;

internal static class LetterCaseHandler
{
    public static IEnumerable<WordModel> ChangeCase(this IEnumerable<WordModel> words, LetterСase letterСase) => letterСase switch
    {
        LetterСase.FirstLetterInUpper   => words.Select(wm => wm with { Word = new string(wm.Word.Select((l, i) => i == 0 ? char.ToUpper(l) : char.ToLower(l)).ToArray()) }),
        LetterСase.AllLetterInUpper     => words.Select(wm => wm with { Word = wm.Word.ToUpper() }),
        LetterСase.AllLetterInLower     => words.Select(wm => wm with { Word = wm.Word.ToLower() }),
        LetterСase.Default or _         => words
    };
}