namespace SGS.HN.Labeler.Service.DTO.ResultModel
{
    /// <summary>
    /// 回傳處理結果
    /// </summary>
    public record ResultModel(bool IsSuccess = true, string? Message = null)
    {
    }
}
