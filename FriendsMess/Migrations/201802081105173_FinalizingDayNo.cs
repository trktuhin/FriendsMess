namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalizingDayNo : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", new[] { "Id", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", "UserId");
        }
    }
}
