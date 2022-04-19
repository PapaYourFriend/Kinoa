using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class OrderDataRepository : BaseRepository<OrderData>, IOrderDataRepository
    {
        public OrderDataRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
