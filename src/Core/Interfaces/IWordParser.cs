namespace WordParser.Core.Interfaces;

internal interface IWordParser
{
    string DetectorMessageError { get; }
    Task<IList<WordModel>> Parse(string url);
    Task<IList<WordModel>> ReParse();
}