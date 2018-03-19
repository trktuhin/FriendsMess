namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiringDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notices", "Details", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notices", "Details", c => c.String());
        }
    }
}
