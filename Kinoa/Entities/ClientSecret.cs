using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kinoa.Entities
{
    public class ClientSecret
    {
        [Key]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public string PasswordEncoded { get; set; }
        [Index("IX_LoginContraint", IsClustered = false, IsUnique = true)]
        [MaxLength(64)]
        public string Login { get; set; }
        [Index("IX_EmailContraint", IsClustered = false, IsUnique = true)]
        [MaxLength(64)]
        public string Email { get; set; }

        public Client Client { get; set; }
    }
}