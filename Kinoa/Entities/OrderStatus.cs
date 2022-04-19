using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}