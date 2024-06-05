namespace SGS.HN.Labeler.Service.DTO.ResultModel
{
    /// <summary>
    /// 列印資料
    /// </summary>
    public record PrintInfoResultModel
    {
        /// <summary>
        /// SL_ID
        /// </summary>
        public string ServiceLineId { get; set; } = "";
        /// <summary>
        /// 條碼列印類型
        /// </summary>
        public BarCodeType BarCodeType { get; set; }
        /// <summary>
        /// (額外)列印資訊
        /// </summary>
        public string?[] PrintInfo { get; set; } = [];
    }
}
