using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
