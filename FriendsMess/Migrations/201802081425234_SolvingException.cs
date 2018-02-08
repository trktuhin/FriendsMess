namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolvingException : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DayNoes", "Id", c => c.Int(nullable: false));
        }
    }
}
