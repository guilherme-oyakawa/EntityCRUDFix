namespace NewGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Fee",
                c => new
                    {
                        FeeID = c.Int(nullable: false, identity: true),
                        RentalID = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, storeType: "money"),
                        Paid = c.Boolean(nullable: false),
                        Client_ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.FeeID)
                .ForeignKey("dbo.Rental", t => t.RentalID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.Client_ClientID)
                .Index(t => t.RentalID)
                .Index(t => t.Client_ClientID);
            
            CreateTable(
                "dbo.Rental",
                c => new
                    {
                        RentalID = c.Int(nullable: false, identity: true),
                        LentOn = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        ReturnedOn = c.DateTime(),
                        ClientID = c.Int(nullable: false),
                        CopyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentalID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Copy", t => t.CopyID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.CopyID);
            
            CreateTable(
                "dbo.Copy",
                c => new
                    {
                        CopyID = c.Int(nullable: false, identity: true),
                        GameID = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CopyID)
                .ForeignKey("dbo.Game", t => t.GameID, cascadeDelete: true)
                .Index(t => t.GameID);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Year = c.DateTime(nullable: false),
                        Description = c.String(),
                        Value = c.Decimal(nullable: false, storeType: "money"),
                        GenreID = c.Int(nullable: false),
                        PublisherID = c.Int(nullable: false),
                        ESRBID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.ESRB", t => t.ESRBID, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Publisher", t => t.PublisherID, cascadeDelete: true)
                .Index(t => t.GenreID)
                .Index(t => t.PublisherID)
                .Index(t => t.ESRBID);
            
            CreateTable(
                "dbo.ESRB",
                c => new
                    {
                        ESRBID = c.Int(nullable: false, identity: true),
                        Rating = c.String(),
                        Description = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ESRBID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.GenreID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PublisherID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PublisherID)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fee", "Client_ClientID", "dbo.Client");
            DropForeignKey("dbo.Fee", "RentalID", "dbo.Rental");
            DropForeignKey("dbo.Rental", "CopyID", "dbo.Copy");
            DropForeignKey("dbo.Copy", "GameID", "dbo.Game");
            DropForeignKey("dbo.Game", "PublisherID", "dbo.Publisher");
            DropForeignKey("dbo.Game", "GenreID", "dbo.Genre");
            DropForeignKey("dbo.Game", "ESRBID", "dbo.ESRB");
            DropForeignKey("dbo.Rental", "ClientID", "dbo.Client");
            DropIndex("dbo.Publisher", new[] { "Name" });
            DropIndex("dbo.Genre", new[] { "Name" });
            DropIndex("dbo.Game", new[] { "ESRBID" });
            DropIndex("dbo.Game", new[] { "PublisherID" });
            DropIndex("dbo.Game", new[] { "GenreID" });
            DropIndex("dbo.Copy", new[] { "GameID" });
            DropIndex("dbo.Rental", new[] { "CopyID" });
            DropIndex("dbo.Rental", new[] { "ClientID" });
            DropIndex("dbo.Fee", new[] { "Client_ClientID" });
            DropIndex("dbo.Fee", new[] { "RentalID" });
            DropTable("dbo.Publisher");
            DropTable("dbo.Genre");
            DropTable("dbo.ESRB");
            DropTable("dbo.Game");
            DropTable("dbo.Copy");
            DropTable("dbo.Rental");
            DropTable("dbo.Fee");
            DropTable("dbo.Client");
        }
    }
}
