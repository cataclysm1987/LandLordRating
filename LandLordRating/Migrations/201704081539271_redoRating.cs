namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redoRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "VoucherUser", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Safty", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "FireExtinguisher", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "SmokeDetectors", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "CarbonMonoxcide", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "LateFees", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "LandLordNotice", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "LandLordResponse", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "ContactPhoneNumer", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "RecommendLandLord", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "RentIncrease", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "WrittenLease", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Eviction", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "EndLease", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "TimesHaveMoved", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "AdditionalComments", c => c.String());
            DropColumn("dbo.Ratings", "RatingDescription");
            DropColumn("dbo.Ratings", "PropertyRating");
            DropColumn("dbo.Ratings", "SafetyRating");
            DropColumn("dbo.Ratings", "CommunicationRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "CommunicationRating", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "SafetyRating", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "PropertyRating", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "RatingDescription", c => c.String());
            DropColumn("dbo.Ratings", "AdditionalComments");
            DropColumn("dbo.Ratings", "TimesHaveMoved");
            DropColumn("dbo.Ratings", "EndLease");
            DropColumn("dbo.Ratings", "Eviction");
            DropColumn("dbo.Ratings", "WrittenLease");
            DropColumn("dbo.Ratings", "RentIncrease");
            DropColumn("dbo.Ratings", "RecommendLandLord");
            DropColumn("dbo.Ratings", "ContactPhoneNumer");
            DropColumn("dbo.Ratings", "LandLordResponse");
            DropColumn("dbo.Ratings", "LandLordNotice");
            DropColumn("dbo.Ratings", "LateFees");
            DropColumn("dbo.Ratings", "CarbonMonoxcide");
            DropColumn("dbo.Ratings", "SmokeDetectors");
            DropColumn("dbo.Ratings", "FireExtinguisher");
            DropColumn("dbo.Ratings", "Safty");
            DropColumn("dbo.Ratings", "VoucherUser");
        }
    }
}
