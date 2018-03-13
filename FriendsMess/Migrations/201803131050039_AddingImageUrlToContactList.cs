namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImageUrlToContactList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactLists", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactLists", "ImageUrl");
        }
    }
}
