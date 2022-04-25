namespace WordParser;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        var host = CreateHostBuilder(Array.Empty<string>()).Build();

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(host.Services.GetRequiredService<WordParserMainForm>());
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureServices(services => services.AddParserComponents());
}