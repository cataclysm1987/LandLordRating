namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class declinedreason : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "DeclinedReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandLords", "DeclinedReason");
        }
    }
}
