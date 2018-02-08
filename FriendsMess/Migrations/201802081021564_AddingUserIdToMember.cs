namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "UserId");
        }
    }
}
