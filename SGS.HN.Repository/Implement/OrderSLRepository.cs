
namespace SGS.HN.Labeler.Repository.Implement;

public class OrderSLRepository(LIMS20_UATContext context) : IOrderSLRepository
{
    public IEnumerable<SLDataModel> GetItems(SLQueryCondition condition)
    {
        var query = context.OrdSls
            .Where(sl => sl.DataState == "N");

        if (string.IsNullOrEmpty(condition.OrderNoEnd))
        {
            query = query.Where(sl => sl.OrdMid == condition.OrderNoStart);
        }
        else
        {
            query = query.Where(sl =>
            string.Compare(sl.OrdMid, condition.OrderNoStart) >= 0 &&
            string.Compare(sl.OrdMid, condition.OrderNoEnd) <= 0);
        }

        var data = query
            .Select(sl => new SLDataModel()
            {
                OrderMId = sl.OrdMid,
                ServiceLineId = sl.SlId
            });

        return data;
    }
}
