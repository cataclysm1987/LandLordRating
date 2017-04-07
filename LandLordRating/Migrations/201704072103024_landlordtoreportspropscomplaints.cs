namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class landlordtoreportspropscomplaints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "LandLord_LandLordId", c => c.Int());
            AddColumn("dbo.Ratings", "LandLord_LandLordId", c => c.Int());
            AddColumn("dbo.Properties", "LandLord_LandLordId", c => c.Int());
            CreateIndex("dbo.Complaints", "LandLord_LandLordId");
            CreateIndex("dbo.Ratings", "LandLord_LandLordId");
            CreateIndex("dbo.Properties", "LandLord_LandLordId");
            AddForeignKey("dbo.Complaints", "LandLord_LandLordId", "dbo.LandLords", "LandLordId");
            AddForeignKey("dbo.Properties", "LandLord_LandLordId", "dbo.LandLords", "LandLordId");
            AddForeignKey("dbo.Ratings", "LandLord_LandLordId", "dbo.LandLords", "LandLordId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "LandLord_LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.Properties", "LandLord_LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.Complaints", "LandLord_LandLordId", "dbo.LandLords");
            DropIndex("dbo.Properties", new[] { "LandLord_LandLordId" });
            DropIndex("dbo.Ratings", new[] { "LandLord_LandLordId" });
            DropIndex("dbo.Complaints", new[] { "LandLord_LandLordId" });
            DropColumn("dbo.Properties", "LandLord_LandLordId");
            DropColumn("dbo.Ratings", "LandLord_LandLordId");
            DropColumn("dbo.Complaints", "LandLord_LandLordId");
        }
    }
}
