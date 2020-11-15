namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedUserAndTicketCommentsTabe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketComments", "Tiket_Id", "dbo.Tickets");
            DropIndex("dbo.TicketComments", new[] { "Tiket_Id" });
            RenameColumn(table: "dbo.TicketComments", name: "Tiket_Id", newName: "TiketId");
            AlterColumn("dbo.TicketComments", "TiketId", c => c.Int(nullable: false));
            CreateIndex("dbo.TicketComments", "TiketId");
            AddForeignKey("dbo.TicketComments", "TiketId", "dbo.Tickets", "Id", cascadeDelete: true);
            DropColumn("dbo.TicketComments", "TiketsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketComments", "TiketsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TicketComments", "TiketId", "dbo.Tickets");
            DropIndex("dbo.TicketComments", new[] { "TiketId" });
            AlterColumn("dbo.TicketComments", "TiketId", c => c.Int());
            RenameColumn(table: "dbo.TicketComments", name: "TiketId", newName: "Tiket_Id");
            CreateIndex("dbo.TicketComments", "Tiket_Id");
            AddForeignKey("dbo.TicketComments", "Tiket_Id", "dbo.Tickets", "Id");
        }
    }
}
