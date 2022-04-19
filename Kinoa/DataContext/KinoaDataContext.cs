using Kinoa.Entities;
using System.Data.Entity;

namespace Kinoa.DataContext
{
    public class KinoaDataContext : DbContext
    {
        public KinoaDataContext() : base("DefaultConnection")
        {

        }

        public DbSet<AgeLimit> AgeLimits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OrderData> OrderDatas { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<FilmRoom> FilmRooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieSession> MovieSessions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}
