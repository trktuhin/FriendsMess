namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingContactList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Location = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactLists");
        }
    }
}
