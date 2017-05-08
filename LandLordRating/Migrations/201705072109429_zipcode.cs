namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zipcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "ZipCode", c => c.String(nullable: false));
            AddColumn("dbo.LandLords", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.LandLords", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "Longitude");
            DropColumn("dbo.LandLords", "Latitude");
            DropColumn("dbo.LandLords", "ZipCode");
        }
    }
}
