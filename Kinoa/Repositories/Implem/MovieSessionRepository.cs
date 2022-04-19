﻿using Kinoa.DataContext;
using Kinoa.Entities;
using Kinoa.Repositories.Base;
using Kinoa.Repositories.Interfaces;

namespace Kinoa.Repositories.Implem
{
    public class MovieSessionRepository : BaseRepository<MovieSession>, IMovieSessionRepository
    {
        public MovieSessionRepository(KinoaDataContext dataContext) : base(dataContext)
        {
        }
    }
}
