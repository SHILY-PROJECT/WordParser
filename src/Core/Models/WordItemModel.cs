namespace WordParser.Core.Models;

internal class WordItemModel
{
    public string? Word { get; set; }
    public int Quantity { get; set; }

    internal WordItemModel(string word, int quantity)
    {
        Word = word;
        Quantity = quantity;
    }
}