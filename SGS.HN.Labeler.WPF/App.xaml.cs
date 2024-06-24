using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IExcelConfigService, ExcelConfigService>();
                    services.AddTransient<IMainViewModel, MainViewModel>();
                    services.AddSingleton<MainWindow>(p => new MainWindow
                    {
                        DataContext = p.GetRequiredService<IMainViewModel>()
                    });
                    // 添加其他服務...
                    services.AddSingleton<IDialogService, DialogService>();
                })
                .Build();
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

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        base.OnStartup(e);
        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
