namespace SGS.HN.Labeler.Service.DTO.ResultModel
{
    /// <summary>
    /// 回傳處理結果
    /// </summary>
    public record ResultModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
