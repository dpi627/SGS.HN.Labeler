using Microsoft.Extensions.Logging;
using SGS.HN.Labeler.Service.DTO.ResultModel;
using SGS.HN.Labeler.Service.Interface;

namespace SGS.HN.Labeler
{
    public partial class frmMain : Form
    {
        private readonly ILogger _logger;
        private readonly IExcelConfigService _excelConfig;

        public frmMain(
            ILogger<frmMain> logger,
            IExcelConfigService excelConfig
            )
        {
            InitializeComponent();
            this._logger = logger;
            this._excelConfig = excelConfig;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("Import Click");
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Select an Excel File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = openFileDialog.FileName;
                string targetPath = Path.Combine(
                    Path.GetDirectoryName(sourcePath),
                    "ExcelConfig"
                );
                ResultModel result = _excelConfig.Import(sourcePath, targetPath);
                if (result.IsSuccess)
                {
                    MessageBox.Show("Import Success");
                }
                else
                {
                    MessageBox.Show("Import Fail: " + result.Message);
                }
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
}