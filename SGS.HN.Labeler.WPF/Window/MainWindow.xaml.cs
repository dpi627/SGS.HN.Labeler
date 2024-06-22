using SGS.HN.Labeler.WPF.ViewModel;
using System.Windows;

namespace SGS.HN.Labeler.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            // 訂閱事件
            viewModel.OrdMidFocused += ViewModel_OrdMidFocused;
        }
    }

    private void ViewModel_OrdMidFocused()
    {
        txtOrderMid.Focus();
    }

    // 當視窗關閉時，取消訂閱事件
    protected override void OnClosed(EventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.OrdMidFocused -= ViewModel_OrdMidFocused;
        }
        base.OnClosed(e);
    }
}