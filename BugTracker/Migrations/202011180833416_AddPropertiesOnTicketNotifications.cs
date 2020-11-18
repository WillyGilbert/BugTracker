namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesOnTicketNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketNotifications", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.TicketNotifications", "ModifiedUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketNotifications", "ModifiedUser");
            DropColumn("dbo.TicketNotifications", "Type");
        }
    }
}
