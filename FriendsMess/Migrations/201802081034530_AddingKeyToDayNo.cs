namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingKeyToDayNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.DayNoes", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", new[] { "Id", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DayNoes");
            AddPrimaryKey("dbo.DayNoes", "Id");
            AlterColumn("dbo.DayNoes", "UserId", c => c.String());
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
