using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Enum;
using SGS.HN.Labeler.Service.Interface;
using SGS.LIB.TscPrinter;

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
            // 讀取設定檔，取得列印資訊
            var printInfo = _excelConfig.Load(selectedConfig.ConfigPath);

            // 依照訂單區間，取得訂單所有SL
            SLInfo slInfo = new()
            {
                OrderNoStart = txtOrderNoStart.Text,
                OrderNoEnd = txtOrderNoEnd.Text
            };
            var slResult = _sl.Query(slInfo);

            TSC.OpenPort("TSC TTP-245");
            TSC.Setup(77, 18);

            try
            {
                foreach (var item in slResult)
                {
                    var p = printInfo.Where(pi => pi.ServiceLineId == item.ServiceLineId).FirstOrDefault();
                    if (p != null)
                    {
                        foreach (string s in p.PrintInfo)
                        {
                            txtOutputMessage.Text = $"{p.BarCodeType} - {item.OrderMid}, {s}" + Environment.NewLine + txtOutputMessage.Text;
                            PrintLabel(p.BarCodeType, item.OrderMid, s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Print Fail");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                TSC.ClosePort();
            }
        }
    }

    private void PrintLabel(BarCodeType barcodeType, string orderNo, string printInfo)
    {
        TSC.ClearBuffer();
        switch (barcodeType)
        {
            case BarCodeType.OrderNoOnly:
                PrintOrderNo(orderNo, printInfo);
                break;
            case BarCodeType.WithPrintInfo:
                PrintWithPrintInfo(orderNo, printInfo);
                break;
        }
        TSC.PrintLabel();
    }

    private void PrintOrderNo(string orderNo, string printInfo)
    {
        TSC.Barcode(24, 0, orderNo);
        TSC.QRCode(24, 45, orderNo);
        TSC.WindowsFont(95, 45, orderNo);
        TSC.WindowsFont(95, 75, printInfo);

        TSC.Barcode(329, 0, orderNo);
        TSC.QRCode(329, 45, orderNo);
        TSC.WindowsFont(400, 45, orderNo);
        TSC.WindowsFont(400, 75, printInfo);
    }

    private void PrintWithPrintInfo(string orderNo, string printInfo)
    {
        var printAll = $"{orderNo}-{printInfo}";
        TSC.Barcode(24, 0, printAll, 1, 1);
        TSC.QRCode(24, 45, printAll, 2);
        TSC.WindowsFont(95, 45, orderNo);
        TSC.WindowsFont(95, 75, printInfo);

        TSC.Barcode(329, 0, printAll, 1, 1);
        TSC.QRCode(329, 45, printAll, 2);
        TSC.WindowsFont(400, 45, orderNo);
        TSC.WindowsFont(400, 75, printInfo);
    }
}