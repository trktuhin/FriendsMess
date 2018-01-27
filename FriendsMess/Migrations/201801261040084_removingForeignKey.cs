namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes");
            DropForeignKey("dbo.Meals", "MemberId", "dbo.Members");
            DropIndex("dbo.Meals", new[] { "DayNoId" });
            DropIndex("dbo.Meals", new[] { "MemberId" });
            RenameColumn(table: "dbo.Meals", name: "DayNoId", newName: "DayNo_Id");
            RenameColumn(table: "dbo.Meals", name: "MemberId", newName: "Member_Id");
            AlterColumn("dbo.Meals", "Member_Id", c => c.Int());
            AlterColumn("dbo.Meals", "DayNo_Id", c => c.Int());
            CreateIndex("dbo.Meals", "DayNo_Id");
            CreateIndex("dbo.Meals", "Member_Id");
            AddForeignKey("dbo.Meals", "DayNo_Id", "dbo.DayNoes", "Id");
            AddForeignKey("dbo.Meals", "Member_Id", "dbo.Members", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.Meals", "DayNo_Id", "dbo.DayNoes");
            DropIndex("dbo.Meals", new[] { "Member_Id" });
            DropIndex("dbo.Meals", new[] { "DayNo_Id" });
            AlterColumn("dbo.Meals", "DayNo_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Meals", "Member_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Meals", name: "Member_Id", newName: "MemberId");
            RenameColumn(table: "dbo.Meals", name: "DayNo_Id", newName: "DayNoId");
            CreateIndex("dbo.Meals", "MemberId");
            CreateIndex("dbo.Meals", "DayNoId");
            AddForeignKey("dbo.Meals", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Meals", "DayNoId", "dbo.DayNoes", "Id", cascadeDelete: true);
        }
    }
}
