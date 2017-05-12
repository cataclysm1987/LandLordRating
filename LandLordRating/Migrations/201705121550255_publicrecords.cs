namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class publicrecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicRecords",
                c => new
                    {
                        PublicRecordId = c.Int(nullable: false, identity: true),
                        RecordType = c.Int(nullable: false),
                        CriminalType = c.Int(),
                        CivilType = c.Int(),
                        DomesticType = c.Int(),
                        CaseName = c.String(nullable: false, maxLength: 40),
                        CaseNumber = c.String(nullable: false, maxLength: 25),
                        DateFiled = c.DateTime(nullable: false),
                        CaseURL = c.String(nullable: false, maxLength: 100),
                        LandLord_LandLordId = c.Int(),
                    })
                .PrimaryKey(t => t.PublicRecordId)
                .ForeignKey("dbo.LandLords", t => t.LandLord_LandLordId)
                .Index(t => t.LandLord_LandLordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicRecords", "LandLord_LandLordId", "dbo.LandLords");
            DropIndex("dbo.PublicRecords", new[] { "LandLord_LandLordId" });
            DropTable("dbo.PublicRecords");
        }
    }
}
