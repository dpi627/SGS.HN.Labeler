namespace SGS.HN.Labeler.WPF.Service;

public interface IDialogService
{
    void ShowMessage(string message, string buttonText = "OK");
    Task ShowMessageAsync(string message, string buttonText = "OK");
    Task<bool> ShowConfirmAsync(string message, string confirmText = "OK", string cancelText = "Cancel");
}
