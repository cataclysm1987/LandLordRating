namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complaintstousers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Complaints", "User_Id");
            AddForeignKey("dbo.Complaints", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Complaints", new[] { "User_Id" });
            DropColumn("dbo.Complaints", "User_Id");
        }
    }
}
