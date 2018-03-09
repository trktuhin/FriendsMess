namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMonthNoToOthers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtherExpenses", "MonthNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OtherExpenses", "MonthNo");
        }
    }
}
