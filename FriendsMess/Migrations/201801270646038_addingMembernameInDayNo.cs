namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMembernameInDayNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayNoes", "ResponsibleMember", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DayNoes", "ResponsibleMember");
        }
    }
}
