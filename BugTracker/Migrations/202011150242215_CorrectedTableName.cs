namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TicketStatuses", newName: "TicketStatus");
            DropForeignKey("dbo.TicketAttachments", "Tikets_Id", "dbo.Tickets");
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            DropIndex("dbo.TicketAttachments", new[] { "Tikets_Id" });
            DropIndex("dbo.TicketAttachments", new[] { "User_Id" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUser_Id" });
            DropIndex("dbo.Tickets", new[] { "OwnerUser_Id" });
            DropIndex("dbo.TicketComments", new[] { "User_Id" });
            DropIndex("dbo.TicketHistories", new[] { "User_Id" });
            DropIndex("dbo.TicketNotifications", new[] { "User_Id" });
            DropColumn("dbo.ProjectUsers", "UserId");
            DropColumn("dbo.TicketAttachments", "UserId");
            DropColumn("dbo.TicketAttachments", "TicketId");
            DropColumn("dbo.Tickets", "AssignedToUserId");
            DropColumn("dbo.Tickets", "OwnerUserId");
            DropColumn("dbo.TicketComments", "UserId");
            DropColumn("dbo.TicketHistories", "UserId");
            DropColumn("dbo.TicketNotifications", "UserId");
            RenameColumn(table: "dbo.ProjectUsers", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.TicketAttachments", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.TicketComments", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.TicketHistories", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.TicketNotifications", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.ProjectUsers", name: "ProjectsId", newName: "ProjectId");
            RenameColumn(table: "dbo.Tickets", name: "AssignedToUser_Id", newName: "AssignedToUserId");
            RenameColumn(table: "dbo.Tickets", name: "OwnerUser_Id", newName: "OwnerUserId");
            RenameColumn(table: "dbo.TicketAttachments", name: "Tikets_Id", newName: "TicketId");
            RenameColumn(table: "dbo.TicketHistories", name: "TiketId", newName: "TicketId");
            RenameColumn(table: "dbo.TicketNotifications", name: "TiketId", newName: "TicketId");
            RenameColumn(table: "dbo.Tickets", name: "TicketTypesId", newName: "TicketTypeId");
            RenameIndex(table: "dbo.ProjectUsers", name: "IX_ProjectsId", newName: "IX_ProjectId");
            RenameIndex(table: "dbo.Tickets", name: "IX_TicketTypesId", newName: "IX_TicketTypeId");
            RenameIndex(table: "dbo.TicketHistories", name: "IX_TiketId", newName: "IX_TicketId");
            RenameIndex(table: "dbo.TicketNotifications", name: "IX_TiketId", newName: "IX_TicketId");
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketAttachments", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketAttachments", "TicketId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketComments", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketHistories", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketNotifications", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectUsers", "UserId");
            CreateIndex("dbo.TicketAttachments", "TicketId");
            CreateIndex("dbo.TicketAttachments", "UserId");
            CreateIndex("dbo.Tickets", "OwnerUserId");
            CreateIndex("dbo.Tickets", "AssignedToUserId");
            CreateIndex("dbo.TicketComments", "UserId");
            CreateIndex("dbo.TicketHistories", "UserId");
            CreateIndex("dbo.TicketNotifications", "UserId");
            AddForeignKey("dbo.TicketAttachments", "TicketId", "dbo.Tickets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketAttachments", "TicketId", "dbo.Tickets");
            DropIndex("dbo.TicketNotifications", new[] { "UserId" });
            DropIndex("dbo.TicketHistories", new[] { "UserId" });
            DropIndex("dbo.TicketComments", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
            DropIndex("dbo.Tickets", new[] { "OwnerUserId" });
            DropIndex("dbo.TicketAttachments", new[] { "UserId" });
            DropIndex("dbo.TicketAttachments", new[] { "TicketId" });
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            AlterColumn("dbo.TicketNotifications", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.TicketHistories", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.TicketComments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.TicketAttachments", "TicketId", c => c.Int());
            AlterColumn("dbo.TicketAttachments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.TicketNotifications", name: "IX_TicketId", newName: "IX_TiketId");
            RenameIndex(table: "dbo.TicketHistories", name: "IX_TicketId", newName: "IX_TiketId");
            RenameIndex(table: "dbo.Tickets", name: "IX_TicketTypeId", newName: "IX_TicketTypesId");
            RenameIndex(table: "dbo.ProjectUsers", name: "IX_ProjectId", newName: "IX_ProjectsId");
            RenameColumn(table: "dbo.Tickets", name: "TicketTypeId", newName: "TicketTypesId");
            RenameColumn(table: "dbo.TicketNotifications", name: "TicketId", newName: "TiketId");
            RenameColumn(table: "dbo.TicketHistories", name: "TicketId", newName: "TiketId");
            RenameColumn(table: "dbo.TicketAttachments", name: "TicketId", newName: "Tikets_Id");
            RenameColumn(table: "dbo.Tickets", name: "OwnerUserId", newName: "OwnerUser_Id");
            RenameColumn(table: "dbo.Tickets", name: "AssignedToUserId", newName: "AssignedToUser_Id");
            RenameColumn(table: "dbo.ProjectUsers", name: "ProjectId", newName: "ProjectsId");
            RenameColumn(table: "dbo.TicketNotifications", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.TicketHistories", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.TicketComments", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.TicketAttachments", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.ProjectUsers", name: "UserId", newName: "User_Id");
            AddColumn("dbo.TicketNotifications", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.TicketHistories", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.TicketComments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "OwnerUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "AssignedToUserId", c => c.Int(nullable: false));
            AddColumn("dbo.TicketAttachments", "TicketId", c => c.Int(nullable: false));
            AddColumn("dbo.TicketAttachments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.TicketNotifications", "User_Id");
            CreateIndex("dbo.TicketHistories", "User_Id");
            CreateIndex("dbo.TicketComments", "User_Id");
            CreateIndex("dbo.Tickets", "OwnerUser_Id");
            CreateIndex("dbo.Tickets", "AssignedToUser_Id");
            CreateIndex("dbo.TicketAttachments", "User_Id");
            CreateIndex("dbo.TicketAttachments", "Tikets_Id");
            CreateIndex("dbo.ProjectUsers", "User_Id");
            AddForeignKey("dbo.TicketAttachments", "Tikets_Id", "dbo.Tickets", "Id");
            RenameTable(name: "dbo.TicketStatus", newName: "TicketStatuses");
        }
    }
}
