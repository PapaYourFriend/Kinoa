using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [MaxLength(48)]
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime Duration { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public DateTime StartDate { get; set; }
        public string Image { get; set; }
        public bool Hot { get; set; } = false;

        public int AgeLimitId { get; set; }
        public AgeLimit AgeLimit { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MovieSession> MovieSessions { get; set; }

    }
}
