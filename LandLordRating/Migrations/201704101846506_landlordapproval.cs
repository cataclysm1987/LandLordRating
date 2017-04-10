namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class landlordapproval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "IsApproved");
        }
    }
}
