namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingCompositeKeyOfAssignedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AssignedDates", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.AssignedDates");
            AddPrimaryKey("dbo.AssignedDates", new[] { "AssignedDay", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AssignedDates");
            AddPrimaryKey("dbo.AssignedDates", "AssignedDay");
            AlterColumn("dbo.AssignedDates", "UserId", c => c.String());
        }
    }
}
