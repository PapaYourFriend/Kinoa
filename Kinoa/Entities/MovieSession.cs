using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class MovieSession
    {
        [Key]
        public int MovieSessionId { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }
        public DateTime SessionDate { get; set; }
        
        public int FilmRoomId { get; set; }
        public FilmRoom FilmRoom { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}