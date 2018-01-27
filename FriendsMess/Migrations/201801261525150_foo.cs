namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meals", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "Id", c => c.Int(nullable: false));
        }
    }
}
