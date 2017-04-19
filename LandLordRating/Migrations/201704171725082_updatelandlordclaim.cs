namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelandlordclaim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandLords", "IsClaimed", c => c.Boolean(nullable: false));
            AddColumn("dbo.LandLords", "IsClaimedDuringCreation", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LandLords", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LandLords", "State", c => c.String(nullable: false));
            DropColumn("dbo.LandLords", "IsClaimedDuringCreation");
            DropColumn("dbo.LandLords", "IsClaimed");
        }
    }
}
