using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
