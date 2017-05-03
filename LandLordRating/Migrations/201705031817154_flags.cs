namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flags",
                c => new
                    {
                        FlagId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        FlaggedObject = c.Int(nullable: false),
                        FlaggedReason = c.Int(nullable: false),
                        FlaggedObjectId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FlagId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flags", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Flags", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Flags");
        }
    }
}
