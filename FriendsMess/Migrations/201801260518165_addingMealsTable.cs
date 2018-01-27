namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMealsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        DayNoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayNoes", t => t.DayNoId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.DayNoId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes");
            DropIndex("dbo.Meals", new[] { "MemberId" });
            DropIndex("dbo.Meals", new[] { "DayNoId" });
            DropTable("dbo.Meals");
        }
    }
}
