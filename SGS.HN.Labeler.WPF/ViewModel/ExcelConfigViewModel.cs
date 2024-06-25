using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.Logging;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Interface;
using SGS.HN.Labeler.WPF.Service;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace SGS.HN.Labeler.WPF.ViewModel;

public partial class ExcelConfigViewModel : ObservableObject
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";
    private string? ExcelConfigRoot;
    private readonly IDialogService _dialog;
    private readonly IExcelConfigService _excelService;

    [ObservableProperty]
    private ObservableCollection<ExcelConfigResultModel> _excelFiles = [];

    [ObservableProperty]
    private ExcelConfigResultModel? _selectedFile;

    public ExcelConfigViewModel(
        IDialogService dialog,
        IExcelConfigService _excelService)
    {
        this._dialog = dialog;
        this._excelService = _excelService;
        SetExcelConfigRoot();
        LoadExcelFiles();
    }

    private void LoadExcelFiles()
    {
        ExcelFiles.Clear();
        foreach (var file in _excelService.GetList(ExcelConfigRoot))
        {
            ExcelFiles.Add(file);
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
            string sourcePath = openFileDialog.FileName;
            string fileName = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(ExcelConfigRoot, fileName);
            ExcelConfigImportInfo info = new(sourcePath, targetPath);
            ResultModel result = _excelService.Import(info);
            if (result.IsSuccess)
            {
                MessageBox.Show("Import Success");
                //_log.LogInformation("Import {@info}", info);
                LoadExcelFiles();
            }
            else
            {
                MessageBox.Show("Import Fail: " + result.Message);
                //_log.LogError("Import Fail: {@info}\n{msg}", info, result.Message);
            }
        }
    }

    [RelayCommand]
    private async Task DeleteExcel()
    {
        if (SelectedFile == default)
        {
            _dialog.ShowMessage("請選擇一個檔案");
            return;
        }

        bool confirmDelete = await _dialog.ShowConfirmAsync("確認刪除?");

        if (!confirmDelete)
            return;

        try
        {
            ResultModel? result = _excelService.Delete(SelectedFile.ConfigPath);
            LoadExcelFiles();
        }
        catch (Exception ex)
        {
            _dialog.ShowMessage(ex.Message);
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
