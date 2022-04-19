using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class FilmRoom
    {
        [Key]
        public int FilmRoomId { get; set; }
        public string FilmRoomName { get; set; }
        public int Capacity { get; set; }
        public int Rows { get; set; }
        public int SeatsInRow { get; set; }
        public bool LoveSeats { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<MovieSession> MovieSessions { get; set; }
    }
}