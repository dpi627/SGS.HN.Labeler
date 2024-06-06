using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SGS.HN.Labeler.Extension;
using Serilog;

namespace SGS.HN.Labeler;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Debug()
            .WriteTo.Seq(serverUrl: "http://twtpeoad002:5341")
            .CreateLogger();

        try
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IServiceCollection? services = new ServiceCollection()
                .AddServices()
                .AddRepositories()
                .AddDbContext(config)
                .AddForms()
                .AddMiscs();

            using ServiceProvider? serviceProvider = services.BuildServiceProvider();
            frmMain? form = serviceProvider.GetOrCreateService<frmMain>();

            Application.Run(form);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
            MessageBox.Show(ex.Message);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}