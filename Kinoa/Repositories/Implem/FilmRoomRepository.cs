using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class FilmRoomRepository : BaseRepository<FilmRoom>, IFilmRoomRepository
    {
        public FilmRoomRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
