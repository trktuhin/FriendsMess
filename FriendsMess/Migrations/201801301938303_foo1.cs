namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OtherExpenses", "Description", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OtherExpenses", "Description", c => c.String());
        }
    }
}
