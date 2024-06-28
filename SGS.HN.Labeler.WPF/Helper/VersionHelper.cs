using System.Reflection;

namespace SGS.HN.Labeler.WPF.Helper;

public class VersionHelper
{
    private static readonly string? _isClickOnce = Environment.GetEnvironmentVariable("ClickOnce_IsNetworkDeployed");
    private static readonly string? _clickOnceVersion = Environment.GetEnvironmentVariable("ClickOnce_CurrentVersion");
    private static readonly string _defaultVersion = "0.0.0.0";

    public static string GetApplicationVersion()
    {
        try
        {
            if (_isClickOnce!=null && Boolean.Parse(_isClickOnce))
            {
                return _clickOnceVersion ?? _defaultVersion;
            }
            else
            {
                return Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            }
        }
        catch (Exception)
        {
            return _defaultVersion;
        }
    }
}
