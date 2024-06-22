using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
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