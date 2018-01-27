namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCompositeKeyToMeal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meals", "Id", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", new[] { "DayNoId", "MemberId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Meals");
            AddPrimaryKey("dbo.Meals", "Id");
            AlterColumn("dbo.Meals", "Id", c => c.Int(nullable: false, identity: true));
        }
    }
}
