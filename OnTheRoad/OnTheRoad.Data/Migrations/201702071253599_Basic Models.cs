namespace OnTheRoad.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToUserId = c.String(maxLength: 128),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ToUserId)
                .Index(t => t.ToUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        CityId = c.Int(),
                        Info = c.String(),
                        ImageId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.UserImages", t => t.ImageId)
                .Index(t => t.CityId)
                .Index(t => t.ImageId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        TripId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.TripId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TripImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TagTrips",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Trip_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Trip_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Tag_Id)
                .Index(t => t.Trip_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Reviews", "ToUserId", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TagTrips", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.TagTrips", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TripImages", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Categories", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ImageId", "dbo.UserImages");
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.TagTrips", new[] { "Trip_Id" });
            DropIndex("dbo.TagTrips", new[] { "Tag_Id" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.TripImages", new[] { "Trip_Id" });
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropIndex("dbo.Subscriptions", new[] { "TripId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "User_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Reviews", new[] { "ToUserId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Categories", new[] { "Trip_Id" });
            DropTable("dbo.TagTrips");
            DropTable("dbo.Roles");
            DropTable("dbo.Tags");
            DropTable("dbo.TripImages");
            DropTable("dbo.Trips");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserImages");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Categories");
        }
    }
}
