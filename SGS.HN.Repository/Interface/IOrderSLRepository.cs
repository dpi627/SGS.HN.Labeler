
namespace SGS.HN.Labeler.Repository.Interface;

public interface IOrderSLRepository
{
    public IEnumerable<SLDataModel> GetItems(SLQueryCondition condition);
}
