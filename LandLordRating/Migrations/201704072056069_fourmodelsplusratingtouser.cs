namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourmodelsplusratingtouser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        ComplaintId = c.Int(nullable: false, identity: true),
                        ComplaintName = c.String(),
                        ComplaintDescription = c.String(),
                    })
                .PrimaryKey(t => t.ComplaintId);
            
            CreateTable(
                "dbo.LandLords",
                c => new
                    {
                        LandLordId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        City = c.String(),
                        State = c.String(),
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
                    })
                .PrimaryKey(t => t.PropertyId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingName = c.String(),
                        RatingDescription = c.String(),
                        PropertyRating = c.Int(nullable: false),
                        LandLordRating = c.Int(nullable: false),
                        SafetyRating = c.Int(nullable: false),
                        CommunicationRating = c.Int(nullable: false),
                        RateAnonymously = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropTable("dbo.Ratings");
            DropTable("dbo.Properties");
            DropTable("dbo.LandLords");
            DropTable("dbo.Complaints");
        }
    }
}
