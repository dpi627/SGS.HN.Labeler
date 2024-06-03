using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Interface;

namespace SGS.HN.Labeler;

public partial class frmMain : Form
{
    private readonly ILogger _log;
    private readonly IExcelConfigService _excelConfig;

    public frmMain(
        ILogger<frmMain> logger,
        IExcelConfigService excelConfig
        )
    {
        InitializeComponent();
        this._log = logger;
        this._excelConfig = excelConfig;
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Select an Excel File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourcePath);
                string targetPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "ExcelConfig",
                    fileName
                );
                ResultModel result = _excelConfig.Import(sourcePath, targetPath);
                if (result.IsSuccess)
                {
                    MessageBox.Show("Import Success");
                    _log.LogInformation("Import {fileName}", fileName);
                }
                else
                {
                    MessageBox.Show("Import Fail: " + result.Message);
                    _log.LogError("Import Fail: {fileName}\n{msg}", fileName, result.Message);
                }
            }
        }
        catch (Exception ex) {
            _log.LogError(ex, "Import Fail");
            MessageBox.Show(ex.Message);
        }
    }

    private void btnSetToDefault_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {

    }
}