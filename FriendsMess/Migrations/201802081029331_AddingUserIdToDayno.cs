namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdToDayno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes");
            DropForeignKey("dbo.Meals", "MemberId", "dbo.Members");
            DropIndex("dbo.Meals", new[] { "DayNoId" });
            DropIndex("dbo.Meals", new[] { "MemberId" });
            AddColumn("dbo.DayNoes", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DayNoes", "UserId");
            CreateIndex("dbo.Meals", "MemberId");
            CreateIndex("dbo.Meals", "DayNoId");
            AddForeignKey("dbo.Meals", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes", "Id", cascadeDelete: true);
        }
    }
}
