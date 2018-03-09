namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDepositTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        MonthNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
            DropColumn("dbo.Members", "Deposit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Deposit", c => c.Int(nullable: false));
            DropForeignKey("dbo.Deposits", "MemberId", "dbo.Members");
            DropIndex("dbo.Deposits", new[] { "MemberId" });
            DropTable("dbo.Deposits");
        }
    }
}
