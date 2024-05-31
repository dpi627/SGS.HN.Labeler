using Microsoft.Extensions.DependencyInjection;
using SGS.HN.Labeler.Extension;

namespace SGS.HN.Labeler;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // 建立 DI 容器，註冊服務
        IServiceCollection? services = new ServiceCollection()
            .AddServices()
            .AddRepositories()
            .AddForms()
            .AddMiscs();

        using ServiceProvider? serviceProvider = services.BuildServiceProvider();
        frmMain? form = serviceProvider.GetOrCreateService<frmMain>();

        Application.Run(form);
    }
}