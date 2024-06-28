namespace SGS.HN.Labeler.Service.DTO.ResultModel
{
    public record ExcelConfigResultModel
    {
        public string? ConfigName { get; set; }
        public string? ConfigPath { get; set; }
        public DateTime LastModified { get; set; }
    }
}
