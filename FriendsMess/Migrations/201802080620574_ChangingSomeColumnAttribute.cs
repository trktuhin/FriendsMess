namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingSomeColumnAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Deposit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Deposit", c => c.Int());
        }
    }
}
