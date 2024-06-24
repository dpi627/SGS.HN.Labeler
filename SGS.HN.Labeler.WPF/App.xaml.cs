using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGS.HN.Labeler.Repository.Implement;
using SGS.HN.Labeler.Repository.Interface;
using SGS.HN.Labeler.Repository.Models;
using SGS.HN.Labeler.Service.Implement;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Pages;
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
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(System.AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(p => new MainWindow
                {
                    DataContext = p.GetRequiredService<MainViewModel>()
                });

                // pages
                services.AddTransient(p => new LabelPrintPage
                {
                    DataContext = p.GetRequiredService<LabelPrintViewModel>()
                });
                services.AddTransient(p => new ExcelConfigPage
                {
                    DataContext = p.GetRequiredService<LabelPrintViewModel>()
                });

                // view models
                services.AddTransient<MainViewModel>();
                services.AddTransient<LabelPrintViewModel>();

                // other services
                services.AddSingleton<IExcelConfigService, ExcelConfigService>();
                services.AddSingleton<ISLService, SLService>();
                services.AddSingleton<IPrintLabelService, PrintLabelService>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IOrderSLRepository, OrderSLRepository>();

                services.AddDbContext<LIMS20_UATContext>(
            options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
            })
            .Build();
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
