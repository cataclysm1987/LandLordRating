namespace LandLordRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingreply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingReplies",
                c => new
                    {
                        RatingReplyId = c.Int(nullable: false),
                        ReplyDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RatingReplyId)
                .ForeignKey("dbo.Ratings", t => t.RatingReplyId)
                .Index(t => t.RatingReplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RatingReplies", "RatingReplyId", "dbo.Ratings");
            DropIndex("dbo.RatingReplies", new[] { "RatingReplyId" });
            DropTable("dbo.RatingReplies");
        }
    }
}
