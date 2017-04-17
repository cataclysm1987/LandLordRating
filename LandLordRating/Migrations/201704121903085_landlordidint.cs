namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class landlordidint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClaimedLandLordId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ClaimedLandLordId");
        }
    }
}
