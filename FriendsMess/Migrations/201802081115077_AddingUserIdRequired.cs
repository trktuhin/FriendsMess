namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdRequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayNoes", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DayNoes", "UserId");
        }
    }
}
