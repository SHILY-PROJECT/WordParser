namespace WordParser.Core.Configuration;

internal static class ParserComponentRegistrar
{
    public static IServiceCollection AddParserComponents(this IServiceCollection services)
    {
        services
            .AddSingleton(s => WordParserSettingsHandler.GetSettings())
            .AddSingleton<IWordParser, WordParserMain>()
            .AddTransient<WordParserMainForm>()
            .AddTransient<WordParserProcessSettingsForm>();

        return services;
    }
}