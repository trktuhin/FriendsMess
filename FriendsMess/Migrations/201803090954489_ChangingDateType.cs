namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingDateType : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE DayNoes DROP CONSTRAINT [DF__DayNoes__DayNumb__3E1D39E1]");
            AlterColumn("dbo.DayNoes", "DayNumber", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Meals", "MemberId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", "MemberId");
            AlterColumn("dbo.Meals", "DayNoId", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", new[] { "DayNoId", "MemberId" });
            AlterColumn("dbo.Meals", "MemberId", c => c.Int(nullable: false));
            AlterColumn("dbo.Meals", "DayNoId", c => c.Int(nullable: false));
            AlterColumn("dbo.DayNoes", "DayNumber", c => c.Int(nullable: false));
        }
    }
}
