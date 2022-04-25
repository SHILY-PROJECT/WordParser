namespace WordParser.Core.Parser.Extensions;

internal static class LetterCaseHandler
{
    public static IEnumerable<WordModel> ChangeCase(this IEnumerable<WordModel> words, LetterСaseEnum letterСase) => letterСase switch
    {
        LetterСaseEnum.FirstLetterInUpper => words.Select(wm => new WordModel(wm.Word != null ? new string(wm.Word.Select((letter, index) => index == 0 ? char.ToUpper(letter) : char.ToLower(letter)).ToArray()) : string.Empty, wm.Quantity)),
        LetterСaseEnum.AllLetterInUpper => words.Select(wm => new WordModel(wm.Word != null ? wm.Word.ToUpper() : string.Empty, wm.Quantity)),
        LetterСaseEnum.AllLetterInLower => words.Select(wm => new WordModel(wm.Word != null ? wm.Word.ToLower() : string.Empty, wm.Quantity)),
        _ or LetterСaseEnum.Default => words
    };
}