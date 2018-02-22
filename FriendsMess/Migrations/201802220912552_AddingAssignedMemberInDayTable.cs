namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAssignedMemberInDayTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayNoes", "AssignedMember", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DayNoes", "AssignedMember");
        }
    }
}
