using System;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class PaymentMethod
    {
        [Key]
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVVEncoded { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}