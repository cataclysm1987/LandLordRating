namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageanddesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "Description", c => c.String());
            AddColumn("dbo.LandLords", "ProfileImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "ProfileImageUrl");
            DropColumn("dbo.LandLords", "Description");
        }
    }
}
