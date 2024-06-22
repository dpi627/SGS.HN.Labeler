namespace SGS.HN.Labeler.WPF.Service;

public interface IDialogService
{
    Task ShowMessageAsync(string message, string buttonText = "OK");
}
