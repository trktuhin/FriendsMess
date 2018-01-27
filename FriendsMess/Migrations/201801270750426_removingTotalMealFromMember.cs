namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingTotalMealFromMember : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "TotalMeal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "TotalMeal", c => c.Int());
        }
    }
}
