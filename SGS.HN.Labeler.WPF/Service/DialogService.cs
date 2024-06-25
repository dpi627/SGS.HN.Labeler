
using MaterialDesignThemes.Wpf;
using SGS.HN.Labeler.WPF.Control;
using SGS.HN.Labeler.WPF.ViewModel;

namespace SGS.HN.Labeler.WPF.Service;

public class DialogService : IDialogService
{
    public void ShowMessage(string message, string buttonText = "OK")
    {
        var view = new DialogView
        {
            DataContext = new DialogViewModel(message, buttonText)
        };

        DialogHost.Show(view, "RootDialog");
    }

    public async Task ShowMessageAsync(string message, string buttonText)
    {
        var view = new DialogView
        {
            DataContext = new DialogViewModel(message, buttonText)
        };

        await DialogHost.Show(view, "RootDialog");
    }

    public async Task<bool> ShowConfirmAsync(string message, string confirmText = "OK", string cancelText = "Cancel")
    {
        var viewModel = new DialogViewModel(message, confirmText, cancelText, true);
        var view = new DialogView
        {
            DataContext = viewModel
        };

        var result = await DialogHost.Show(view, "RootDialog");
        return result is bool boolResult && boolResult;
    }
}
