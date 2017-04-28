namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandLordClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimName = c.String(),
                        ClaimDescription = c.String(),
                        DocumentFilePath = c.String(),
                        IsPending = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        LandLord_LandLordId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.LandLords", t => t.LandLord_LandLordId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.LandLord_LandLordId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClaimedLandLordId = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingName = c.String(nullable: false),
                        RatingDescription = c.String(nullable: false),
                        LateFees = c.Int(nullable: false),
                        LandLordNotice = c.Int(nullable: false),
                        LandLordResponse = c.Int(nullable: false),
                        ContactPhoneNumer = c.Int(nullable: false),
                        RecommendLandLord = c.Int(nullable: false),
                        RentIncrease = c.Int(nullable: false),
                        LandLordRating = c.Int(nullable: false),
                        RateAnonymously = c.Int(nullable: false),
                        LandLordId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsDeclined = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.LandLords", t => t.LandLordId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.LandLordId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LandLords",
                c => new
                    {
                        LandLordId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Description = c.String(),
                        ProfileImageUrl = c.String(),
                        LandLordOrTenant = c.Int(nullable: false),
                        IndividualOrCompany = c.Int(nullable: false),
                        OverallRating = c.Double(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsClaimed = c.Boolean(nullable: false),
                        IsDeclined = c.Boolean(nullable: false),
                        IsClaimedDuringCreation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LandLordId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        PropertyDescription = c.String(),
                        Beds = c.Int(nullable: false),
                        Baths = c.Int(nullable: false),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        LandLord_LandLordId = c.Int(),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.LandLords", t => t.LandLord_LandLordId)
                .Index(t => t.LandLord_LandLordId);
            
            CreateTable(
                "dbo.RatingReplies",
                c => new
                    {
                        RatingReplyId = c.Int(nullable: false),
                        ReplyDescription = c.String(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsDeclined = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RatingReplyId)
                .ForeignKey("dbo.Ratings", t => t.RatingReplyId)
                .Index(t => t.RatingReplyId);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.LandLordClaims", "LandLord_LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.RatingReplies", "RatingReplyId", "dbo.Ratings");
            DropForeignKey("dbo.Ratings", "LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.Properties", "LandLord_LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.LandLordClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RatingReplies", new[] { "RatingReplyId" });
            DropIndex("dbo.Properties", new[] { "LandLord_LandLordId" });
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropIndex("dbo.Ratings", new[] { "LandLordId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.LandLordClaims", new[] { "LandLord_LandLordId" });
            DropIndex("dbo.LandLordClaims", new[] { "ApplicationUser_Id" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.RatingReplies");
            DropTable("dbo.Properties");
            DropTable("dbo.LandLords");
            DropTable("dbo.Ratings");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.LandLordClaims");
        }
    }
}
