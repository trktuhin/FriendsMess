namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMobileNumberInMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "MobileNumber");
        }
    }
}
