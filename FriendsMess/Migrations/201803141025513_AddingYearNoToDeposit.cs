namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingYearNoToDeposit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deposits", "YearNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deposits", "YearNo");
        }
    }
}
