using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Models;
using SGS.HN.Labeler.Service.DTO.Info;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Enum;
using SGS.HN.Labeler.Service.Interface;
using SGS.OAD.TscPrinter;
using System.Drawing.Printing;

namespace SGS.HN.Labeler;

public partial class frmMain : Form
{
    static private readonly string ExcelConfigDirectory = "ExcelConfig";

    private readonly ILogger _log;
    private readonly IExcelConfigService _excelConfig;
    private readonly ISLService _sl;
    private string ExcelConfigRoot;

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
        SetExcelConfigRoot();
        SetPrinter();
        SetExcelConfigList();
        SetExcelConfigDropDownList();
    }

    private void SetExcelConfigRoot()
    {
        if (!Directory.Exists(ExcelConfigDirectory))
        {
            Directory.CreateDirectory(ExcelConfigDirectory);
        }
        ExcelConfigRoot = Path.Combine(Directory.GetCurrentDirectory(), ExcelConfigDirectory);
    }

    private void SetPrinter()
    {
        // Clear the existing items in the cbbPrinter control
        cbbPrinter.Items.Clear();

        // Get the names of all installed printers
        string[] printerNames = PrinterSettings
            .InstalledPrinters
            .Cast<string>()
            .Where(x => x.StartsWith("TSC"))
            .ToArray();

        // Add the printer names to the cbbPrinter control
        cbbPrinter.Items.AddRange(printerNames);

        // Select the first printer in the list, if available
        if (cbbPrinter.Items.Count > 0)
        {
            cbbPrinter.SelectedIndex = 0;
        }
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
        string? printer = cbbPrinter.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(printer))
        {
            MessageBox.Show("請選擇印表機");
            return;
        }

        if (cbbExcelConfig.SelectedItem is null)
        {
            MessageBox.Show("請選擇列印設定檔");
            return;
        }

        // Get the selected item from the dropdown list
        if (cbbExcelConfig.SelectedItem is ExcelConfigResultModel selectedConfig)
        {
            //TODO: 如果檔案沒有接換，可以不要每次都讀取
            // 讀取 Excel 設定檔，取得列印資訊
            IEnumerable<PrintInfoResultModel>? excelPrintInfo = _excelConfig.Load(selectedConfig.ConfigPath!);

            // 依照訂單區間，取得訂單所有SL
            SLInfo slInfo = new()
            {
                OrderNoStart = txtOrderNoStart.Text,
                OrderNoEnd = txtOrderNoEnd.Text
            };
            IEnumerable<SLResultModel>? slResult = _sl.Query(slInfo);

            TSC.Build(printer, 73, 15);

            try
            {
                int labelCount = 0;
                foreach (var sl in slResult)
                {
                    PrintInfoResultModel? printInfoRow = excelPrintInfo
                        .Where(pi => pi.ServiceLineId == sl.ServiceLineId)
                        .FirstOrDefault();

                    if (printInfoRow != null)
                    {
                        foreach (string? printInfo in printInfoRow.PrintInfo)
                        {
                            labelCount++;
                            txtOutputMessage.Text = $"{labelCount:0000} {sl.OrderMid}, {printInfo}" + Environment.NewLine + txtOutputMessage.Text;
                            SetLabel(printInfoRow.BarCodeType, sl.OrderMid!, printInfo!, labelCount);

                            if (labelCount % 2 == 0)
                                TSC.Print();
                        }
                    }
                }
                //檢查如果labelCount是奇數，表示最後一筆資料只有一張標籤，需再列印一次
                if (labelCount % 2 != 0)
                    TSC.Print();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Print Fail");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                TSC.Dispose();
            }
        }
    }

    private static void SetLabel(BarCodeType type, string orderNo, string printInfo, int labelCount = 0)
    {
        PrintParam pp = SetPrintParam(type, orderNo, printInfo);
        if (labelCount % 2 != 0)
            SetContent(pp, 10, 10);
        else
            SetContent(pp, 314, 10);
    }

    /// <summary>
    /// 列印參數處理，例如資料串接
    /// </summary>
    /// <param name="OrdMid">訂單編號</param>
    /// <param name="PrintInfo">列印資訊</param>
    /// <param name="OnlyOrdMid">是否只包含訂單編號</param>
    /// <returns></returns>
    private static PrintParam SetPrintParam(BarCodeType type, string OrdMid, string PrintInfo)
    {
        var qrcode = type == BarCodeType.WithPrintInfo ? OrdMid : $"{OrdMid}-{PrintInfo}";
        var barcode = type == BarCodeType.WithPrintInfo ? OrdMid : qrcode;
        return new PrintParam(OrdMid, PrintInfo, qrcode, barcode);
    }

    /// <summary>
    /// 設定列印內容與座標
    /// </summary>
    /// <param name="pp">列印參數</param>
    /// <param name="offsetX">X座標位移</param>
    /// <param name="offsetY">Y座標位移</param>
    private static void SetContent(PrintParam pp, int offsetX, int offsetY)
    {
        TSC.Barcode(offsetX, offsetY, pp.Barcode, height: 45);
        TSC.Qrcode(offsetX, offsetY + 55, pp.Qrcode);
        TSC.WindowsFont(offsetX + 55, offsetY + 50, pp.OrdMid, 24, "Consolas");
        TSC.WindowsFont(offsetX + 55, offsetY + 70, pp.PrintInfo, 24, "Consolas");
    }
}