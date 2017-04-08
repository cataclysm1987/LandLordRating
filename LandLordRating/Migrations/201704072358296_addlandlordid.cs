namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlandlordid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "LandLord_LandLordId", "dbo.LandLords");
            DropIndex("dbo.Ratings", new[] { "LandLord_LandLordId" });
            RenameColumn(table: "dbo.Ratings", name: "LandLord_LandLordId", newName: "LandLordId");
            AlterColumn("dbo.Ratings", "LandLordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "LandLordId");
            AddForeignKey("dbo.Ratings", "LandLordId", "dbo.LandLords", "LandLordId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "LandLordId", "dbo.LandLords");
            DropIndex("dbo.Ratings", new[] { "LandLordId" });
            AlterColumn("dbo.Ratings", "LandLordId", c => c.Int());
            RenameColumn(table: "dbo.Ratings", name: "LandLordId", newName: "LandLord_LandLordId");
            CreateIndex("dbo.Ratings", "LandLord_LandLordId");
            AddForeignKey("dbo.Ratings", "LandLord_LandLordId", "dbo.LandLords", "LandLordId");
        }
    }
}
