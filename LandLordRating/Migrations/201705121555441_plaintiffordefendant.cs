namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plaintiffordefendant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicRecords", "PlaintiffOrDefendant", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublicRecords", "PlaintiffOrDefendant");
        }
    }
}
