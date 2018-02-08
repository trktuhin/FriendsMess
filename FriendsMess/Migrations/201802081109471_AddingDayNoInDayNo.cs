namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDayNoInDayNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayNoes", "DayNumber", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", "Id");
            DropColumn("dbo.DayNoes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayNoes", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", new[] { "Id", "UserId" });
            DropColumn("dbo.DayNoes", "DayNumber");
        }
    }
}
