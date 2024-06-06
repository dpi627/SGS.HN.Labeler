namespace SGS.HN.Labeler.Service.Interface;

public interface ISLService
{
    public IEnumerable<SLResultModel> Query(SLInfo info);
}
