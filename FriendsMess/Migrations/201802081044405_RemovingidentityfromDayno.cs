namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingidentityfromDayno : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", "UserId");
            AlterColumn("dbo.DayNoes","Id", c => c.Int(identity:false));
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", new[] { "Id", "UserId" });
        }
    }
}
