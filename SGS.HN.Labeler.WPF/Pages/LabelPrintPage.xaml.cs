using SGS.HN.Labeler.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SGS.HN.Labeler.WPF.Pages;

/// <summary>
/// Interaction logic for LabelPrintPage.xaml
/// </summary>
public partial class LabelPrintPage : Page
{
    public LabelPrintPage()
    {
        InitializeComponent();
        Loaded += LabelPrintPage_Loaded;
        Unloaded += LabelPrintPage_Unloaded;
    }

    private void LabelPrintPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is LabelPrintViewModel viewModel)
        {
            // 訂閱事件
            viewModel.OrdMidFocused += ViewModel_OrdMidFocused;
            ViewModel_OrdMidFocused();
        }
    }

    private void ViewModel_OrdMidFocused()
    {
        txtOrderMid.Focus();
    }

    // 當視窗關閉時，取消訂閱事件
    private void LabelPrintPage_Unloaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is LabelPrintViewModel viewModel)
        {
            viewModel.OrdMidFocused -= ViewModel_OrdMidFocused;
        }
    }
}
