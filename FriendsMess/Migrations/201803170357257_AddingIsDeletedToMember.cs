namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsDeletedToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "IsDeleted");
        }
    }
}
