namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakingDayNOIdKeyAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meals", "MemberId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", new[] { "MemberId", "DayNoId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", "MemberId");
            AlterColumn("dbo.Meals", "MemberId", c => c.Int(nullable: false, identity: true));
        }
    }
}
