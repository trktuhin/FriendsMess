namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdInMealTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "UserId");
        }
    }
}
