﻿namespace SGS.HN.Labeler.Service.Implement
{
    public class ExcelConfigService : IExcelConfigService
    {
        public IEnumerable<ExcelConfigResultModel> GetList(string directoryPath)
        {
            IEnumerable<ExcelConfigResultModel>? files = Directory
                .GetFiles(directoryPath, "*.xlsx")
                .Select(filePath => new ExcelConfigResultModel
                {
                    ConfigName = Path.GetFileNameWithoutExtension(filePath),
                    ConfigPath = filePath
                });
            return files;
        }

        public ResultModel Import(string sourcePath, string targetPath)
        {
            try
            {
                File.Copy(sourcePath, targetPath, true);
            }
            catch (Exception ex)
            {
                return new ResultModel(
                    IsSuccess:false,
                    Message:ex.Message
                    );
            }
            return new ResultModel();
        }

        public IEnumerable<PrintInfoResultModel> Load(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
