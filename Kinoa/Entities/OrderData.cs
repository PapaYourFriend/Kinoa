using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class OrderData
    {
        [Key]
        public int OrderDataId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
