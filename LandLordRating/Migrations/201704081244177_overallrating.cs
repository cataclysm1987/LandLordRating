namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class overallrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "OverallRating", c => c.Double(nullable: false));
            AlterColumn("dbo.LandLords", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.LandLords", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.LandLords", "City", c => c.String(nullable: false));
            AlterColumn("dbo.LandLords", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LandLords", "State", c => c.String());
            AlterColumn("dbo.LandLords", "City", c => c.String());
            AlterColumn("dbo.LandLords", "PhoneNumber", c => c.String());
            AlterColumn("dbo.LandLords", "FullName", c => c.String());
            DropColumn("dbo.LandLords", "OverallRating");
        }
    }
}
