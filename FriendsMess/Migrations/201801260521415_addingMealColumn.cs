namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMealColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "MealNo", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "MealNo");
        }
    }
}
