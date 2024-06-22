using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SGS.HN.Labeler.Service.Interface;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";
    private readonly IExcelConfigService _excelConfig;
    private string ExcelConfigRoot;

    [ObservableProperty]
    private ObservableCollection<string> printers;

    [ObservableProperty]
    private string? selectedPrinter;

    [ObservableProperty]
    private ObservableCollection<string> excelConfigs;

    [ObservableProperty]
    private string? selectedExcelConfig;

    [ObservableProperty]
    private string? orderMid;

    [ObservableProperty]
    private string? printHistory;

    [ObservableProperty]
    private bool controlsEnabled = true;

    [ObservableProperty]
    private bool isClearButtonVisible = false;

    public MainViewModel(IExcelConfigService ExcelConfig)
    {
        this._excelConfig = ExcelConfig;
        SetExcelConfigRoot();
        this.ExcelConfigs = new ObservableCollection<string>(
            _excelConfig.GetList(ExcelConfigRoot)
            .Select(x => x.ConfigName)!);

        Printers = new ObservableCollection<string>(
            PrinterSettings.InstalledPrinters
                           .Cast<string>()
                           .Where(x => x.StartsWith("TSC")));

        // 預設第一台印表機
        if (Printers.Any())
            SelectedPrinter = Printers.First();

        // 預設第一個Excel配置
        if (ExcelConfigs.Any())
            SelectedExcelConfig = ExcelConfigs.First();`
    }

    [RelayCommand]
    private void PrintLabel()
    {
        // 實現打印標籤的邏輯
        System.Diagnostics.Debug.WriteLine($"Selected Printer: {SelectedPrinter}");
        System.Diagnostics.Debug.WriteLine($"Selected Excel Config: {SelectedExcelConfig}");
        System.Diagnostics.Debug.WriteLine($"Order MID: {OrderMid}");
        PrintHistory += $"Printed: {OrderMid} on {SelectedPrinter} with {SelectedExcelConfig}\n";
        IsClearButtonVisible = true;
    }

    [RelayCommand]
    private void ClearHistory()
    {
        // 實現清除歷史記錄的邏輯
        OrderMid = string.Empty;
        SelectedPrinter = null;
        SelectedExcelConfig = null;
        PrintHistory = string.Empty;
        IsClearButtonVisible = false;
    }

    private void SetExcelConfigRoot()
    {
        if (!Directory.Exists(ExcelConfigDirectory))
        {
            Directory.CreateDirectory(ExcelConfigDirectory);
        }
        ExcelConfigRoot = Path.Combine(Directory.GetCurrentDirectory(), ExcelConfigDirectory);
    }
}