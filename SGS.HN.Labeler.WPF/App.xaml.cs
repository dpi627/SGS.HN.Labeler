using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SGS.HN.Labeler.Repository.Implement;
using SGS.HN.Labeler.Repository.Interface;
using SGS.HN.Labeler.Repository.Models;
using SGS.HN.Labeler.Service.Implement;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Helper;
using SGS.HN.Labeler.WPF.Pages;
using SGS.HN.Labeler.WPF.Service;
using SGS.HN.Labeler.WPF.ViewModel;
using System.Windows;

namespace SGS.HN.Labeler.WPF;

public partial class App : Application
{

    private static string? _environment;
    private static string? _appName;
    public static IHost? AppHost { get; private set; }

    public App()
    {
        var builder = Host.CreateApplicationBuilder();

        _environment = builder.Environment.EnvironmentName;
        _appName = builder.Environment.ApplicationName;

        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // 設定 Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration) // 讀取設定檔中的日誌設定
            .Enrich.WithProperty("Version", VersionHelper.CurrentVersion) // 版本無法寫在設定檔
            .Enrich.WithProperty("Application", _appName) //應用程式名稱不寫於設定檔
            .CreateLogger();

        try
        {
            Log.Information("{Application} 於 {EnvironmentName} 啟動", _appName, _environment);

            // 清除預設的日誌提供者
            builder.Logging.ClearProviders();
            // 使用 Serilog 取代內建的日誌機制
            builder.Logging.AddSerilog();

            builder.Services.AddSingleton(p => new MainWindow
            {
                DataContext = p.GetRequiredService<MainViewModel>()
            });

            // pages
            builder.Services.AddTransient(p => new LabelPrintPage
            {
                DataContext = p.GetRequiredService<LabelPrintViewModel>()
            });
            builder.Services.AddTransient(p => new ExcelConfigPage
            {
                DataContext = p.GetRequiredService<ExcelConfigViewModel>()
            });

            // view models
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LabelPrintViewModel>();
            builder.Services.AddTransient<ExcelConfigViewModel>();

            // other services
            builder.Services.AddSingleton<IExcelConfigService, ExcelConfigService>();
            builder.Services.AddSingleton<ISLService, SLService>();
            builder.Services.AddSingleton<IPrintLabelService, PrintLabelService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<IOrderSLRepository, OrderSLRepository>();

            builder.Services.AddDbContext<LIMS20_UATContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{Application} 於 {EnvironmentName} 異常", _appName, _environment);
        }

        AppHost = builder.Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();
        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        //await AppHost!.StopAsync();
        Log.Information("{Application} {Version} 關閉");
        using (AppHost)
        {
            await AppHost!.StopAsync();
        }
        Log.CloseAndFlush(); // 在應用結束時關閉 Serilog
        base.OnExit(e);
    }
}
