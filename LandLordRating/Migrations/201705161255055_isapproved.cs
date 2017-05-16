namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isapproved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicRecords", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublicRecords", "IsDeclined", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublicRecords", "IsDeclined");
            DropColumn("dbo.PublicRecords", "IsApproved");
        }
    }
}
