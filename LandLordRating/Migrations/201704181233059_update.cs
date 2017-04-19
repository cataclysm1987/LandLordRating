namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "LandLordOrTenant", c => c.Int(nullable: false));
            AddColumn("dbo.LandLords", "IndividualOrCompany", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "IndividualOrCompany");
            DropColumn("dbo.LandLords", "LandLordOrTenant");
        }
    }
}
