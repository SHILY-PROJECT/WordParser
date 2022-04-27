namespace WordParser.Core.Configuration;

internal static class ParserComponentRegistrar
{
    public static IServiceCollection AddParserComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<IWordParserSettingsHandler, WordParserSettingsHandler>()
            .AddSingleton<IWordParser, WordParserMain>()
            .AddTransient<WordParserMainForm>()
            .AddTransient<WordParserProcessSettingsForm>()
            .AddTransient<WaitForm>();

        return services;
    }
}