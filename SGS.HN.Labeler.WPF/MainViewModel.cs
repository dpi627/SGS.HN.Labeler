using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Printing;

namespace SGS.HN.Labeler.WPF;

internal class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<string> Printers { get; set; }

    private string _selectedPrinter;
    public string SelectedPrinter
    {
        get { return _selectedPrinter; }
        set
        {
            if (_selectedPrinter != value)
            {
                _selectedPrinter = value;
                OnPropertyChanged(nameof(SelectedPrinter));
            }
        }
    }

    public MainViewModel()
    {
        Printers = new ObservableCollection<string>(
        PrinterSettings.InstalledPrinters
                       .Cast<string>()
                       .Where(x => x.StartsWith("TSC")));

        // 默認選擇第一個項目
        if (Printers.Any())
        {
            SelectedPrinter = Printers.First();
        }

    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
