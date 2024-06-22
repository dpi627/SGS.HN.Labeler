
using MaterialDesignThemes.Wpf;
using SGS.HN.Labeler.WPF.Control;
using SGS.HN.Labeler.WPF.Model;

namespace SGS.HN.Labeler.WPF.Service;

public class DialogService : IDialogService
{
    public async Task ShowMessageAsync(string message, string buttonText)
    {
        var view = new DialogView
        {
            DataContext = new DialogViewModel(message, buttonText)
        };

        await DialogHost.Show(view, "RootDialog");
    }
}
