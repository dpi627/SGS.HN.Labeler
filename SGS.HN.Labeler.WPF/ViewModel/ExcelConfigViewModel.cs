using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SGS.HN.Labeler.Service.Interface;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class ExcelConfigViewModel : ObservableObject
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";
    private string? ExcelConfigRoot;

    private readonly IExcelConfigService _excelService;

    [ObservableProperty]
    private ObservableCollection<string> _excelFiles = [];

    [ObservableProperty]
    private string? _selectedFile;

    public ExcelConfigViewModel(IExcelConfigService _excelService)
    {
        this._excelService = _excelService;
        SetExcelConfigRoot();
        LoadExcelFiles();
    }

    private void LoadExcelFiles()
    {
        ExcelFiles.Clear();
        foreach (var file in _excelService.GetList(ExcelConfigRoot))
        {
            ExcelFiles.Add(file.ConfigName);
        }
    }

    [RelayCommand]
    private void ImportExcel()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            Title = "Select an Excel file"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            //_excelService.ImportExcelFile(openFileDialog.FileName);
            LoadExcelFiles();
        }
    }

    [RelayCommand]
    private void DeleteExcel()
    {
        if (SelectedFile != null)
        {
            //_excelService.DeleteExcelFile(SelectedFile);
            LoadExcelFiles();
        }
    }

    private void SetExcelConfigRoot()
    {
        if (!Directory.Exists(ExcelConfigDirectory))
        {
            Directory.CreateDirectory(ExcelConfigDirectory);
        }
        ExcelConfigRoot = Path.Combine(Directory.GetCurrentDirectory(), ExcelConfigDirectory);
    }
}
