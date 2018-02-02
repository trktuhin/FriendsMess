namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCompositeKeyToDayNo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes");
            DropIndex("dbo.Meals", new[] { "DayNoId" });
            AddColumn("dbo.DayNoes", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Meals", "DayNo_Id", c => c.Int());
            AddColumn("dbo.Meals", "DayNo_UserName", c => c.String(maxLength: 128));
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", new[] { "Id", "UserName" });
            CreateIndex("dbo.Meals", new[] { "DayNo_Id", "DayNo_UserName" });
            AddForeignKey("dbo.Meals", new[] { "DayNo_Id", "DayNo_UserName" }, "dbo.DayNoes", new[] { "Id", "UserName" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", new[] { "DayNo_Id", "DayNo_UserName" }, "dbo.DayNoes");
            DropIndex("dbo.Meals", new[] { "DayNo_Id", "DayNo_UserName" });
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", "Id");
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Meals", "DayNo_UserName");
            DropColumn("dbo.Meals", "DayNo_Id");
            DropColumn("dbo.DayNoes", "UserName");
            CreateIndex("dbo.Meals", "DayNoId");
            AddForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes", "Id", cascadeDelete: true);
        }
    }
}
