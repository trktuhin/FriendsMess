namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdToAssignedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignedDates", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignedDates", "UserId");
        }
    }
}
