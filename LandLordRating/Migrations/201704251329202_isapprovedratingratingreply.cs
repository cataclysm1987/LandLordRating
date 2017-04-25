namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isapprovedratingratingreply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "IsDeclined", c => c.Boolean(nullable: false));
            AddColumn("dbo.RatingReplies", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.RatingReplies", "IsDeclined", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RatingReplies", "IsDeclined");
            DropColumn("dbo.RatingReplies", "IsApproved");
            DropColumn("dbo.Ratings", "IsDeclined");
            DropColumn("dbo.Ratings", "IsApproved");
        }
    }
}
