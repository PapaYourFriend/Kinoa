using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class AgeLimit
    {
        [Key]
        public int AgeLimitId { get; set; }
        public string AgeLimitName { get; set; }
        public string Image { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}