namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class landlordclaim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandLordClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimName = c.String(),
                        ClaimDescription = c.String(),
                        DocumentFilePath = c.String(),
                        IsPending = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        LandLord_LandLordId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.LandLords", t => t.LandLord_LandLordId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.LandLord_LandLordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LandLordClaims", "LandLord_LandLordId", "dbo.LandLords");
            DropForeignKey("dbo.LandLordClaims", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LandLordClaims", new[] { "LandLord_LandLordId" });
            DropIndex("dbo.LandLordClaims", new[] { "ApplicationUser_Id" });
            DropTable("dbo.LandLordClaims");
        }
    }
}
