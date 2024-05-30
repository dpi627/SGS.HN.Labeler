namespace SGS.HN.Labeler.Service.Interface
{
    public interface IExcelConfigService
    {
        public IEnumerable<ExcelConfigResultModel> GetList(string directoryPath);
        public ResultModel Import(string sourcePath, string targetPath);
        public IEnumerable<PrintInfoResultModel> Load(string filePath);
    }
}
