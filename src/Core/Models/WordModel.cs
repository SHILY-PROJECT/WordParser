namespace WordParser.Core.Models;

internal class WordModel
{
    public string? Word { get; set; }
    public int Quantity { get; set; }

    internal WordModel(string word, int quantity)
    {
        Word = word;
        Quantity = quantity;
    }
}