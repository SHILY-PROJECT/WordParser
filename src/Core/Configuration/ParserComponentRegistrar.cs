namespace WordParser.Core.Configuration;

internal static class ParserComponentRegistrar
{
    public static IServiceCollection AddParserComponents(this IServiceCollection services)
    {
        var wordParserProcessSettings = WordParserSettingsHandler.GetSettings();

        services
            .AddSingleton(s => wordParserProcessSettings)
            .AddSingleton<WordParserMainForm>()
            .AddSingleton<IWordParser, WordParserMain>();

        return services;
    }
}