namespace WordParser.Core.Models;

internal record WordModel : IEqualityComparer<WordModel>
{
    public string Word { get; init; } = string.Empty;
    public int Quantity { get; init; }

    bool IEqualityComparer<WordModel>.Equals(WordModel? firstWordModel, WordModel? secondWordModel)
    {
        return firstWordModel?.Word?.Equals(secondWordModel?.Word, StringComparison.OrdinalIgnoreCase) ?? throw new Exception();
    }

    int IEqualityComparer<WordModel>.GetHashCode(WordModel wordModel)
    {
        return HashCode.Combine(Word.GetHashCode(), wordModel.Word);
    }
}