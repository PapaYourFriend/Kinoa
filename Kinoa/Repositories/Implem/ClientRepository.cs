using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
