using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
