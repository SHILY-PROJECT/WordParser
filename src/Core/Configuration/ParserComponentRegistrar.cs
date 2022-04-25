namespace WordParser.Core.Configuration;

internal static class ParserComponentRegistrar
{
    public static IServiceCollection AddParserComponents(this IServiceCollection services)
    {
        var wordParserProcessSettings = WordParserSettingsHandler.GetSettings();
        var wordParser = new WordParserMain(wordParserProcessSettings);

        services
            .AddSingleton(s => wordParserProcessSettings)
            .AddSingleton<IWordParser, WordParserMain>(s => wordParser)
            .AddSingleton<WordParserMainForm>(s => new WordParserMainForm(wordParser, wordParserProcessSettings));

        return services;
    }
}