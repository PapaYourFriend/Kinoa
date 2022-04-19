using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kinoa.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Image { get; set; }

        public ClientSecret ClientSecret { get; set; }
        public ICollection<PaymentMethod> PaymendMethods { get; set; }

    }
}
