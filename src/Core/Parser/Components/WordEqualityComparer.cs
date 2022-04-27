namespace WordParser.Core.Parser.Components;

internal class WordEqualityComparer : IEqualityComparer<WordModel>
{
    bool IEqualityComparer<WordModel>.Equals(WordModel? firstWord, WordModel? secondWord)
    {
        return firstWord?.Word?.Equals(secondWord?.Word, StringComparison.OrdinalIgnoreCase) ?? throw new Exception();
    }

    int IEqualityComparer<WordModel>.GetHashCode(WordModel wordModel)
    {
        return wordModel.Word.IndexOf(wordModel.Word, StringComparison.OrdinalIgnoreCase);
    }
}