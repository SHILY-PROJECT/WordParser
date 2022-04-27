namespace WordParser.Core.Parser.Components;

internal class WordParseSettingsChangesEventArgs : EventArgs
{
    public ParserMode ParserMode { get; }

    public WordParseSettingsChangesEventArgs(ParserMode parserMode)
    {
        ParserMode = parserMode;
    }
}