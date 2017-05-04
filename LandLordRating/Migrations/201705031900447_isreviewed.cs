namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isreviewed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flags", "IsReviewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flags", "IsReviewed");
        }
    }
}
