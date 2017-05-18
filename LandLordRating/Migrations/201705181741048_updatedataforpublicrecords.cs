namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedataforpublicrecords : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublicRecords", "CaseURL", c => c.String(nullable: false, maxLength: 120));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublicRecords", "CaseURL", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
