﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Enum;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Model;
using SGS.HN.Labeler.WPF.Service;
using SGS.OAD.TscPrinter;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class LabelPrintViewModel : ObservableObject
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";
    private readonly IExcelConfigService _excelConfig;
    private readonly ISLService _sl;
    private readonly IDialogService _dialog;
    private string ExcelConfigRoot;
    private ILogger _logger;
    private Stopwatch _watchPrint = new();
    private bool _isTest = false; //測試模式使用，避免直接列印浪費紙，或甚至沒接印表機

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

    public LabelPrintViewModel(
        IExcelConfigService ExcelConfig,
        ISLService SLService,
        IDialogService dialog,
        ILogger<LabelPrintViewModel> logger)
    {
#if DEBUG
        _isTest = true;
#endif
        _excelConfig = ExcelConfig;
        _sl = SLService;
        _dialog = dialog;
        _logger = logger;

        SetExcelConfigRoot();

        var data = _excelConfig
            .GetList(ExcelConfigRoot)
            .Select(x => new ComboBoxItem(x.ConfigName, x.ConfigPath));
        ExcelConfigs = new ObservableCollection<ComboBoxItem>(data);

        _logger.LogInformation("Set Excels: {@ExcelConfigs}", ExcelConfigs);

        Printers = new ObservableCollection<string>(
            PrinterSettings.InstalledPrinters
                           .Cast<string>()
                           .Where(x => x.StartsWith("TSC")));

        _logger.LogInformation("Set Printers: {@Printers}", Printers);

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

        // 讀取 Excel 設定檔，取得列印資訊
        IEnumerable<PrintInfoResultModel>? excelPrintInfo = _excelConfig.Load(SelectedExcelConfig.Value);

        if (excelPrintInfo == null)
        {
            _dialog.ShowMessage("讀取Excel設定檔失敗", isAutoClose: true);
            _logger.LogError("Load Excel Config Fail: {ExcelConfig}", SelectedExcelConfig);
            return;
        }

        // 依照訂單區間，取得訂單所有SL
        SLInfo slInfo = new() { OrderNoStart = OrderMid };
        IEnumerable<SLResultModel>? slResult = _sl.Query(slInfo);

        if (!slResult.Any())
        {
            _dialog.ShowMessage("查無資料", isAutoClose: true);
            _logger.LogWarning("No Data Found: {OrderMid}", OrderMid);
            return;
        }

        _watchPrint.Restart();
        TSC.Build(SelectedPrinter, 73, 15);

        _logger.LogInformation("Build Printer: {SelectedPrinter} (Test Mode: {IsTest})", SelectedPrinter, _isTest);

        try
        {
            _logger.LogInformation("Print Start: {OrderMid}", OrderMid);
            int labelCount = 0;
            foreach (var sl in slResult)
            {
                PrintInfoResultModel? printInfoRow = excelPrintInfo
                    .Where(pi => pi.ServiceLineId.Trim() == sl.ServiceLineId)
                    .FirstOrDefault();

                if (printInfoRow != null)
                {
                    foreach (string? printInfo in printInfoRow.PrintInfo)
                    {
                        _logger.LogInformation("Set Label #{Seq} {OrderNo} {PrintInfo}", labelCount, OrderMid, printInfo);

                        labelCount++;

                        PrintHistory = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Printed: {OrderMid}:{printInfo}\n{PrintHistory}";

                        SetLabel(printInfoRow.BarCodeType, sl.OrderMid!, printInfo!, labelCount);

                        if (labelCount % 2 == 0 && !_isTest)
                            TSC.Print();
                    }
                }
            }
            //檢查如果labelCount是奇數，表示最後一筆資料只有一張標籤，需再列印一次
            if (labelCount % 2 != 0 && !_isTest)
                TSC.Print();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Print Fail: {OrderMid}", OrderMid);
            _dialog.ShowMessage(ex.Message);
        }
        finally
        {
            TSC.Dispose();
            _watchPrint.Stop();
            _logger.LogInformation("Print End: {OrderMid} ({Elapsed}ms)", OrderMid, _watchPrint.ElapsedMilliseconds);
        }

        //PrintHistory = $"Printed: {OrderMid} on {SelectedPrinter} with {SelectedExcelConfig.Text}\n{PrintHistory}";
        //PrintHistory = $"Printed: {OrderMid}\n{PrintHistory}";
        IsClearButtonVisible = true;
        OrderMid = string.Empty;
        OrdMidFocused?.Invoke();
    }

    private static void SetLabel(BarCodeType type, string orderNo, string printInfo, int labelCount = 0)
    {
        PrintParam pp = SetPrintParam(type, orderNo, printInfo);
        if (labelCount % 2 != 0)
            SetContent(pp, 10, 10, type);
        else
            SetContent(pp, 314, 10, type);
    }

    /// <summary>
    /// 列印參數處理，例如資料串接
    /// </summary>
    /// <param name="OrdMid">訂單編號</param>
    /// <param name="PrintInfo">列印資訊</param>
    /// <param name="OnlyOrdMid">是否只包含訂單編號</param>
    /// <returns></returns>
    private static PrintParam SetPrintParam(BarCodeType type, string OrdMid, string PrintInfo)
    {
        OrdMid = OrdMid.Trim();
        PrintInfo = PrintInfo.Trim();
        var qrcode = type == BarCodeType.OrderNoOnly ? OrdMid : $"{OrdMid}-{PrintInfo}";
        var barcode = type == BarCodeType.OrderNoOnly ? OrdMid : qrcode;
        return new PrintParam(OrdMid, PrintInfo, qrcode, barcode);
    }

    /// <summary>
    /// 設定列印內容與座標
    /// </summary>
    /// <param name="pp">列印參數</param>
    /// <param name="offsetX">X座標位移</param>
    /// <param name="offsetY">Y座標位移</param>
    private static void SetContent(PrintParam pp, int offsetX, int offsetY, BarCodeType type)
    {
        int qrOffset = type == BarCodeType.OrderNoOnly ? 15 : 0; //純印訂單編號QR比較小，靠右多10

        TSC.Barcode(offsetX, offsetY, pp.Barcode, height: 45);
        TSC.Qrcode(offsetX + 205 + qrOffset, offsetY + 50, pp.Qrcode);
        TSC.WindowsFont(offsetX, offsetY + 45, pp.OrdMid, 32, "Consolas");
        TSC.WindowsFont(offsetX, offsetY + 70, pp.PrintInfo, 32, "Consolas");
    }

    /// <summary>
    /// 左邊填充空白，製造靠右對齊效果
    /// </summary>
    /// <param name="input"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    private static string PadLeft(string input, int len = 14) =>
        string.IsNullOrEmpty(input) ? "" : input.PadLeft(len);

    [RelayCommand]
    private async Task OrderMidEnter()
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
