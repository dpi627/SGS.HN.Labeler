using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Model;

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
    private ObservableCollection<ComboBoxItem> excelConfigs;

    [ObservableProperty]
    private ComboBoxItem? selectedExcelConfig;

    [ObservableProperty]
    private string? orderMid;

    [ObservableProperty]
    private string? printHistory;

    [ObservableProperty]
    private bool controlsEnabled = true;

    [ObservableProperty]
    private bool isClearButtonVisible = false;

    [ObservableProperty]
    private bool isPrintButtonEnabled = true;

    [ObservableProperty]
    private bool isAutoPrint;

    public event Action? OrdMidFocused;

    partial void OnIsAutoPrintChanged(bool value)
    {
        if (value)
        {
            OrdMidFocused?.Invoke();
        }
    }

    public MainViewModel(IExcelConfigService ExcelConfig)
    {
        _excelConfig = ExcelConfig;
        SetExcelConfigRoot();

        var data = _excelConfig
            .GetList(ExcelConfigRoot)
            .Select(x => new ComboBoxItem(x.ConfigName, x.ConfigPath));
        ExcelConfigs = new ObservableCollection<ComboBoxItem>(data);

        Printers = new ObservableCollection<string>(
            PrinterSettings.InstalledPrinters
                           .Cast<string>()
                           .Where(x => x.StartsWith("TSC")));

        // 預設第一台印表機
        if (Printers.Any())
            SelectedPrinter = Printers.First();

        // 預設第一個Excel配置
        if (ExcelConfigs.Any())
            SelectedExcelConfig = ExcelConfigs.First();
    }

    [RelayCommand]
    private void TogglePrintButton(bool isChecked)
    {
        IsPrintButtonEnabled = !isChecked;
    }

    [RelayCommand]
    private void PrintLabel()
    {
        // 實現打印標籤的邏輯
        Debug.WriteLine($"Selected Printer: {SelectedPrinter}");
        Debug.WriteLine($"Selected Excel Config: {SelectedExcelConfig}");
        Debug.WriteLine($"Order MID: {OrderMid}");
        PrintHistory = $"Printed: {OrderMid} on {SelectedPrinter} with {SelectedExcelConfig}\n{PrintHistory}";
        IsClearButtonVisible = true;
        OrderMid = string.Empty;
    }

    [RelayCommand]
    private void OrderMidEnter()
    {
        Debug.WriteLine(OrderMid);

        if (IsAutoPrint && !string.IsNullOrEmpty(OrderMid))
            PrintLabel();
    }

    [RelayCommand]
    private void ClearHistory()
    {
        // 實現清除歷史記錄的邏輯
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