using Microsoft.Extensions.DependencyInjection;
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

            IServiceCollection? services = new ServiceCollection()
                .AddServices()
                .AddRepositories()
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