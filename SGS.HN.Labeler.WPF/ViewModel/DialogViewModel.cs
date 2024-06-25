using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Windows.Forms;
using System.Windows.Input;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class DialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string _message;

    [ObservableProperty]
    private string _okButtonText;

    [ObservableProperty]
    private string _cancelButtonText;

    [ObservableProperty]
    private bool _isOkButtonVisible;

    [ObservableProperty]
    private bool _isCancelButtonVisible;

    public ICommand OkCommand { get; }
    public ICommand CancelCommand { get; }

    public DialogViewModel(string message, string okButtonText = "OK", string cancelButtonText = "Cancel", bool showCancelButton = false)
    {
        Message = message;
        OkButtonText = okButtonText;
        CancelButtonText = cancelButtonText;
        IsOkButtonVisible = true;
        IsCancelButtonVisible = showCancelButton;

        OkCommand = new RelayCommand(() => CloseDialog(true));
        CancelCommand = new RelayCommand(() => CloseDialog(false));
    }

    private void CloseDialog(bool result)
    {
        DialogHost.Close("RootDialog", result);
    }
}