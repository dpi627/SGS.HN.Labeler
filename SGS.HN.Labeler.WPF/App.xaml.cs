using Microsoft.Extensions.DependencyInjection;
using SGS.HN.Labeler.Service.Implement;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Service;
using SGS.HN.Labeler.WPF.ViewModel;
using System.Windows;

namespace SGS.HN.Labeler.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; }

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IExcelConfigService, ExcelConfigService>();
        services.AddTransient<IMainViewModel, MainViewModel>();
        services.AddTransient<MainWindow>(p => new MainWindow
        {
            DataContext = p.GetRequiredService<IMainViewModel>()
        });
        // 添加其他服務...
        services.AddSingleton<IDialogService, DialogService>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
