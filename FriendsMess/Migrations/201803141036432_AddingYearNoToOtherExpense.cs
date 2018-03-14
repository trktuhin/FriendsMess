namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingYearNoToOtherExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherExpenses", "YearNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OtherExpenses", "YearNo");
        }
    }
}
