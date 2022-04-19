using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
