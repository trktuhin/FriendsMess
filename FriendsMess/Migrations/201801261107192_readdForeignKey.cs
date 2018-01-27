namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "DayNo_Id", "dbo.DayNoes");
            DropForeignKey("dbo.Meals", "Member_Id", "dbo.Members");
            DropIndex("dbo.Meals", new[] { "DayNo_Id" });
            DropIndex("dbo.Meals", new[] { "Member_Id" });
            RenameColumn(table: "dbo.Meals", name: "DayNo_Id", newName: "DayNoId");
            RenameColumn(table: "dbo.Meals", name: "Member_Id", newName: "MemberId");
            AlterColumn("dbo.Meals", "DayNoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Meals", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Meals", "DayNoId");
            CreateIndex("dbo.Meals", "MemberId");
            AddForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Meals", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes");
            DropIndex("dbo.Meals", new[] { "MemberId" });
            DropIndex("dbo.Meals", new[] { "DayNoId" });
            AlterColumn("dbo.Meals", "MemberId", c => c.Int());
            AlterColumn("dbo.Meals", "DayNoId", c => c.Int());
            RenameColumn(table: "dbo.Meals", name: "MemberId", newName: "Member_Id");
            RenameColumn(table: "dbo.Meals", name: "DayNoId", newName: "DayNo_Id");
            CreateIndex("dbo.Meals", "Member_Id");
            CreateIndex("dbo.Meals", "DayNo_Id");
            AddForeignKey("dbo.Meals", "Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.Meals", "DayNo_Id", "dbo.DayNoes", "Id");
        }
    }
}
