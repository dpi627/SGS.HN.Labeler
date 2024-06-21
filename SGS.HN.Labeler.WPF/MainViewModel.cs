using System.Collections.ObjectModel;
using System.Drawing.Printing;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SGS.HN.Labeler.WPF;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> printers;

    [ObservableProperty]
    private string? selectedPrinter;

    public MainViewModel()
    {
        Printers = new ObservableCollection<string>(
            PrinterSettings.InstalledPrinters
                           .Cast<string>()
                           .Where(x => x.StartsWith("TSC")));

        // 預設第一台印表機
        if (Printers.Any())
            SelectedPrinter = Printers.First();
    }
}