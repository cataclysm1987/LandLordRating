namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeunwantedprops : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "RatingDescription", c => c.String());
            DropColumn("dbo.Ratings", "VoucherUser");
            DropColumn("dbo.Ratings", "Safty");
            DropColumn("dbo.Ratings", "FireExtinguisher");
            DropColumn("dbo.Ratings", "SmokeDetectors");
            DropColumn("dbo.Ratings", "CarbonMonoxcide");
            DropColumn("dbo.Ratings", "WrittenLease");
            DropColumn("dbo.Ratings", "Eviction");
            DropColumn("dbo.Ratings", "EndLease");
            DropColumn("dbo.Ratings", "TimesHaveMoved");
            DropColumn("dbo.Ratings", "AdditionalComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "AdditionalComments", c => c.String());
            AddColumn("dbo.Ratings", "TimesHaveMoved", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "EndLease", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Eviction", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "WrittenLease", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "CarbonMonoxcide", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "SmokeDetectors", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "FireExtinguisher", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Safty", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "VoucherUser", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "RatingDescription");
        }
    }
}
