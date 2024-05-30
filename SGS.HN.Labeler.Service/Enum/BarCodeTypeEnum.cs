namespace SGS.HN.Labeler.Service.Enum
{
    /// <summary>
    /// 條碼列印類型，適用二維與QR
    /// </summary>
    public enum BarCodeType
    {
        /// <summary>
        /// 預設，訂單編號
        /// </summary>
        OrderNo = 0,
        /// <summary>
        /// 訂單編號+列印資訊
        /// </summary>
        WithPrintInfo = 1
    }
}
