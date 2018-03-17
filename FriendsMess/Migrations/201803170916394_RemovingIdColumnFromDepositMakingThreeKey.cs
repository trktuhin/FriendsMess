namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingIdColumnFromDepositMakingThreeKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Deposits");
            AddPrimaryKey("dbo.Deposits", new[] { "MemberId", "MonthNo", "YearNo" });
            DropColumn("dbo.Deposits", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deposits", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Deposits");
            AddPrimaryKey("dbo.Deposits", "Id");
        }
    }
}
