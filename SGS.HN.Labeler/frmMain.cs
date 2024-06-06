using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Interface;

namespace SGS.HN.Labeler;

public partial class frmMain : Form
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";

    private readonly ILogger _log;
    private readonly IExcelConfigService _excelConfig;
    private readonly ISLService _sl;
    private readonly string ExcelConfigRoot = Path.Combine(Directory.GetCurrentDirectory(), ExcelConfigDirectory);

    public frmMain(
        ILogger<frmMain> logger,
        IExcelConfigService excelConfig,
        ISLService sl
        )
    {
        InitializeComponent();
        this._log = logger;
        this._excelConfig = excelConfig;
        this._sl = sl;
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
        SetExcelConfigList();
        SetExcelConfigDropDownList();
    }

    private void SetExcelConfigDropDownList()
    {
        IEnumerable<ExcelConfigResultModel>? data = _excelConfig.GetList(ExcelConfigRoot);
        cbbExcelConfig.DataSource = data.ToList();
        cbbExcelConfig.DisplayMember = "ConfigName";
        cbbExcelConfig.ValueMember = "ConfigPath";
    }

    private void SetExcelConfigList()
    {
        IEnumerable<ExcelConfigResultModel>? data = _excelConfig.GetList(ExcelConfigRoot);
        dgvConfig.DataSource = data.ToList();
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
                string targetPath = Path.Combine(ExcelConfigRoot, fileName);
                ExcelConfigImportInfo info = new(sourcePath, targetPath);
                ResultModel result = _excelConfig.Import(info);
                if (result.IsSuccess)
                {
                    MessageBox.Show("Import Success");
                    _log.LogInformation("Import {@info}", info);
                    SetExcelConfigList();
                    SetExcelConfigDropDownList();
                }
                else
                {
                    MessageBox.Show("Import Fail: " + result.Message);
                    _log.LogError("Import Fail: {@info}\n{msg}", info, result.Message);
                }
            }
        }
        catch (Exception ex)
        {
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

    private void btnPrint_Click(object sender, EventArgs e)
    {
        // Get the selected item from the dropdown list
        if (cbbExcelConfig.SelectedItem is ExcelConfigResultModel selectedConfig)
        {
            var printInfo = _excelConfig.Load(selectedConfig.ConfigPath);
            foreach (var item in printInfo)
            {
                txtOutputMessage.Text = item + Environment.NewLine + txtOutputMessage.Text;
                foreach (string s in item.PrintInfo)
                {
                    txtOutputMessage.Text = s + Environment.NewLine + txtOutputMessage.Text;
                }
            }
        }

        SLInfo slInfo = new()
        {
            OrderNoStart = txtOrderNoStart.Text,
            OrderNoEnd = txtOrderNoEnd.Text
        };
        var slResult = _sl.Query(slInfo);
        foreach (var item in slResult)
        {
            txtOutputMessage.Text = item + Environment.NewLine + txtOutputMessage.Text;
        }
    }
}