namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameColumnInExtraExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherExpenses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.OtherExpenses", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OtherExpenses", "Description", c => c.String(nullable: false));
            DropColumn("dbo.OtherExpenses", "Name");
        }
    }
}
