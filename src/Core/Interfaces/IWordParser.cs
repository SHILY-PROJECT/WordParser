namespace WordParser.Core.Interfaces;

internal interface IWordParser
{
    Task<IList<WordModel>> Parse(string url);
    Task<IList<WordModel>> ReApplyFilterAsync();
}