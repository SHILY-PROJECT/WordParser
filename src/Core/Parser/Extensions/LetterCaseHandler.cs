namespace WordParser.Core.Parser.Extensions;

internal static class LetterCaseHandler
{
    public static IEnumerable<WordModel> ChangeCase(this IEnumerable<WordModel> words, LetterCase letterСase) => letterСase switch
    {
        LetterCase.FirstLetterInUpper   => words.Select(wm => wm with { Word = new string(wm.Word.Select((l, i) => i == 0 ? char.ToUpper(l) : char.ToLower(l)).ToArray()) }),
        LetterCase.AllLetterInUpper     => words.Select(wm => wm with { Word = wm.Word.ToUpper() }),
        LetterCase.AllLetterInLower     => words.Select(wm => wm with { Word = wm.Word.ToLower() }),
        LetterCase.Default or _         => words
    };
}