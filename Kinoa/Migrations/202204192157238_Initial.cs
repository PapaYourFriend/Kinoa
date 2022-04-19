namespace Kinoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeLimits",
                c => new
                    {
                        AgeLimitId = c.Int(nullable: false, identity: true),
                        AgeLimitName = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.AgeLimitId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 48),
                        Title = c.String(),
                        Duration = c.DateTime(nullable: false),
                        Description = c.String(),
                        Rating = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Image = c.String(),
                        Hot = c.Boolean(nullable: false),
                        AgeLimitId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.AgeLimits", t => t.AgeLimitId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.AgeLimitId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.MovieSessions",
                c => new
                    {
                        MovieSessionId = c.Int(nullable: false, identity: true),
                        SessionStartTime = c.DateTime(nullable: false),
                        SessionEndTime = c.DateTime(nullable: false),
                        SessionDate = c.DateTime(nullable: false),
                        FilmRoomId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieSessionId)
                .ForeignKey("dbo.FilmRooms", t => t.FilmRoomId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.FilmRoomId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.FilmRooms",
                c => new
                    {
                        FilmRoomId = c.Int(nullable: false, identity: true),
                        FilmRoomName = c.String(),
                        Capacity = c.Int(nullable: false),
                        Rows = c.Int(nullable: false),
                        SeatsInRow = c.Int(nullable: false),
                        LoveSeats = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FilmRoomId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientId = c.Int(nullable: false),
                        MovieSessionId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.MovieSessions", t => t.MovieSessionId, cascadeDelete: true)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.MovieSessionId)
                .Index(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ClientSecrets",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        PasswordEncoded = c.String(),
                        Login = c.String(maxLength: 64),
                        Email = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.Login, unique: true, name: "IX_LoginContraint")
                .Index(t => t.Email, unique: true, name: "IX_EmailContraint");
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        CardNumber = c.String(nullable: false, maxLength: 128),
                        CardHolderName = c.String(),
                        CVVEncoded = c.String(),
                        ExpireMonth = c.Int(nullable: false),
                        ExpireYear = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardNumber)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.OrderDatas",
                c => new
                    {
                        OrderDataId = c.Int(nullable: false, identity: true),
                        RowNumber = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDataId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        OrderStatusName = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Orders", "MovieSessionId", "dbo.MovieSessions");
            DropForeignKey("dbo.OrderDatas", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.PaymentMethods", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientSecrets", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.MovieSessions", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieSessions", "FilmRoomId", "dbo.FilmRooms");
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Movies", "AgeLimitId", "dbo.AgeLimits");
            DropIndex("dbo.OrderDatas", new[] { "OrderId" });
            DropIndex("dbo.PaymentMethods", new[] { "ClientId" });
            DropIndex("dbo.ClientSecrets", "IX_EmailContraint");
            DropIndex("dbo.ClientSecrets", "IX_LoginContraint");
            DropIndex("dbo.ClientSecrets", new[] { "ClientId" });
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropIndex("dbo.Orders", new[] { "MovieSessionId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.MovieSessions", new[] { "MovieId" });
            DropIndex("dbo.MovieSessions", new[] { "FilmRoomId" });
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropIndex("dbo.Movies", new[] { "AgeLimitId" });
            DropTable("dbo.OrderStatus");
            DropTable("dbo.OrderDatas");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.ClientSecrets");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.FilmRooms");
            DropTable("dbo.MovieSessions");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.AgeLimits");
        }
    }
}
