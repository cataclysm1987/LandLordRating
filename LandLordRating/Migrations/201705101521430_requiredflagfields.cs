namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredflagfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flags", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flags", "Description", c => c.String());
        }
    }
}
