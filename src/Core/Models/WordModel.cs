namespace WordParser.Core.Models;

internal record WordModel
{
    public string Word { get; init; } = string.Empty;
    public int Quantity { get; init; }
}