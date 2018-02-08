namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToOtherExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherExpenses", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OtherExpenses", "UserId");
        }
    }
}
