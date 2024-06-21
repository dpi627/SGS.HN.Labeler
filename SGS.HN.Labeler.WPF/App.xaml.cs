using Microsoft.Extensions.DependencyInjection;
using SGS.HN.Labeler.Service.Implement;
using SGS.HN.Labeler.Service.Interface;
using System.Windows;

namespace SGS.HN.Labeler.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IExcelConfigService, ExcelConfigService>();
        services.AddTransient<MainViewModel>();
        // 添加其他服務...
    }

    public IServiceProvider ServiceProvider { get; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = new MainWindow
        {
            DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
        };

        mainWindow.Show();
    }
}
