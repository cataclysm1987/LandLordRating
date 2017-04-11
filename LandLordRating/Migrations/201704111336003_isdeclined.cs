namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isdeclined : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "IsDeclined", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "IsDeclined");
        }
    }
}
