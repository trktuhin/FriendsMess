namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdInMealTableToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meals", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meals", "UserId", c => c.String(nullable: false));
        }
    }
}
