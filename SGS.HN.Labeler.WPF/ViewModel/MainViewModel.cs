using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Model;
using SGS.HN.Labeler.WPF.Service;
using System.Text.RegularExpressions;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class MainViewModel : ObservableObject, IMainViewModel
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";
    private readonly IExcelConfigService _excelConfig;
    private readonly IDialogService _dialog;
    private string ExcelConfigRoot;

    [ObservableProperty]
    private ObservableCollection<string> printers;

    [ObservableProperty]
    private string? selectedPrinter;

    [ObservableProperty]
    private ObservableCollection<ComboBoxItem> excelConfigs;

    [ObservableProperty]
    private ComboBoxItem? selectedExcelConfig;

    #region 不使用 [ObservableProperty]，直接寫屬性內容
    /* 為了將資料轉大寫，使用完整寫法，非 Attribute 方式
     * 必須使用 field 搭配 public property
     * setter 中使用 SetProperty(ref field, value) 來觸發 PropertyChanged 事件
     */
    private string? orderMid;
    public string? OrderMid
    {
        get => orderMid;
        set => SetProperty(ref orderMid, value == null ? value : value.ToUpper());
    }
    #endregion

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

    public MainViewModel(IExcelConfigService ExcelConfig, IDialogService dialog)
    {
        _excelConfig = ExcelConfig;
        _dialog = dialog;
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
    private async Task PrintLabel()
    {
        if (string.IsNullOrWhiteSpace(selectedPrinter))
        {
            await _dialog.ShowMessageAsync("請選擇印表機");
            return;
        }
        if (selectedExcelConfig == default)
        {
            await _dialog.ShowMessageAsync("請選擇設定檔");
            return;
        }
        if (string.IsNullOrWhiteSpace(orderMid))
        {
            await _dialog.ShowMessageAsync("訂單編號不可空白");
            return;
        }
        else
        {
            var orderMidPattern = @"^[A-Z]{3}\d{2}[A-C0-9][0-9]{5,}$";
            if (!Regex.IsMatch(orderMid, orderMidPattern))
            {
                await _dialog.ShowMessageAsync("訂單編號格式不正確");
                return;
            }
        }

        // 實現打印標籤的邏輯
        Debug.WriteLine($"Selected Printer: {SelectedPrinter}");
        Debug.WriteLine($"Selected Excel Config: {SelectedExcelConfig}");
        Debug.WriteLine($"Order MID: {OrderMid}");
        //PrintHistory = $"Printed: {OrderMid} on {SelectedPrinter} with {SelectedExcelConfig.Text}\n{PrintHistory}";
        PrintHistory = $"Printed: {OrderMid}\n{PrintHistory}";
        IsClearButtonVisible = true;
        OrderMid = string.Empty;
    }

    [RelayCommand]
    private async void OrderMidEnter()
    {
        Debug.WriteLine(OrderMid);

        if (IsAutoPrint && !string.IsNullOrEmpty(OrderMid))
            await PrintLabel();
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