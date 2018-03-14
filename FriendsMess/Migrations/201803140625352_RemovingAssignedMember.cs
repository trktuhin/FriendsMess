namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingAssignedMember : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DayNoes", "AssignedMember");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayNoes", "AssignedMember", c => c.String());
        }
    }
}
