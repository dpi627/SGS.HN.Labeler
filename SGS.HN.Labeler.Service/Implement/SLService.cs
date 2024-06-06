

namespace SGS.HN.Labeler.Service.Implement;

public class SLService(IOrderSLRepository repo) : ISLService
{
    public IEnumerable<SLResultModel> Query(SLInfo info)
    {
        SLQueryCondition condition = new()
        {
            OrderNoStart = info.OrderNoStart,
            OrderNoEnd = info.OrderNoEnd
        };

        IEnumerable<SLDataModel>? data = repo.GetItems(condition);

        IEnumerable<SLResultModel>? result = data
            .Select(sl => new SLResultModel()
            {
                OrderMid = sl.OrderMId,
                ServiceLineId = sl.ServiceLineId
            });

        return result;
    }
}
