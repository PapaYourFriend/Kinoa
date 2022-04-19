using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Sum { get; set; }

        public ICollection<OrderData> Data { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int MovieSessionId { get; set; }
        public MovieSession MovieSession { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
