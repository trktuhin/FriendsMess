namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignedDateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedDates",
                c => new
                    {
                        AssignedDay = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignedDay)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedDates", "MemberId", "dbo.Members");
            DropIndex("dbo.AssignedDates", new[] { "MemberId" });
            DropTable("dbo.AssignedDates");
        }
    }
}
