namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBugTrakerDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProjectsId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TicketAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        FilePath = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        FileUrl = c.String(),
                        Tikets_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Tikets_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Tikets_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        TicketTypesId = c.Int(nullable: false),
                        TicketPriorityId = c.Int(nullable: false),
                        TicketStatusId = c.Int(nullable: false),
                        OwnerUserId = c.Int(nullable: false),
                        AssignedToUserId = c.Int(nullable: false),
                        AssignedToUser_Id = c.String(maxLength: 128),
                        OwnerUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedToUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerUser_Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.TicketPriorities", t => t.TicketPriorityId, cascadeDelete: true)
                .ForeignKey("dbo.TicketStatuses", t => t.TicketStatusId, cascadeDelete: true)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypesId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ProjectId)
                .Index(t => t.TicketTypesId)
                .Index(t => t.TicketPriorityId)
                .Index(t => t.TicketStatusId)
                .Index(t => t.AssignedToUser_Id)
                .Index(t => t.OwnerUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TicketComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Created = c.DateTime(nullable: false),
                        TiketsId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Tiket_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Tiket_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Tiket_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TicketHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TiketId = c.Int(nullable: false),
                        Property = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        Changed = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TiketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.TiketId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TicketNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TiketId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TiketId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.TiketId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TicketPriorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketStatuses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketAttachments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "TicketTypesId", "dbo.TicketTypes");
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses");
            DropForeignKey("dbo.Tickets", "TicketPriorityId", "dbo.TicketPriorities");
            DropForeignKey("dbo.TicketNotifications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketNotifications", "TiketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketHistories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketHistories", "TiketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketComments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketComments", "Tiket_Id", "dbo.Tickets");
            DropForeignKey("dbo.TicketAttachments", "Tikets_Id", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "OwnerUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "AssignedToUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectUsers", "ProjectsId", "dbo.Projects");
            DropIndex("dbo.TicketTypes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketStatuses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TicketNotifications", new[] { "User_Id" });
            DropIndex("dbo.TicketNotifications", new[] { "TiketId" });
            DropIndex("dbo.TicketHistories", new[] { "User_Id" });
            DropIndex("dbo.TicketHistories", new[] { "TiketId" });
            DropIndex("dbo.TicketComments", new[] { "User_Id" });
            DropIndex("dbo.TicketComments", new[] { "Tiket_Id" });
            DropIndex("dbo.Tickets", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Tickets", new[] { "OwnerUser_Id" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUser_Id" });
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            DropIndex("dbo.Tickets", new[] { "TicketPriorityId" });
            DropIndex("dbo.Tickets", new[] { "TicketTypesId" });
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            DropIndex("dbo.TicketAttachments", new[] { "User_Id" });
            DropIndex("dbo.TicketAttachments", new[] { "Tikets_Id" });
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            DropIndex("dbo.ProjectUsers", new[] { "ProjectsId" });
            DropIndex("dbo.Projects", new[] { "ApplicationUser_Id" });
            DropTable("dbo.TicketTypes");
            DropTable("dbo.TicketStatuses");
            DropTable("dbo.TicketPriorities");
            DropTable("dbo.TicketNotifications");
            DropTable("dbo.TicketHistories");
            DropTable("dbo.TicketComments");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketAttachments");
            DropTable("dbo.ProjectUsers");
            DropTable("dbo.Projects");
        }
    }
}
