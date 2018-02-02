namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUsernameToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "UserName");
        }
    }
}
