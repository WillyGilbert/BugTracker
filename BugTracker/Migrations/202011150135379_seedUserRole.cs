namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class seedUserRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
Delete [dbo].[AspNetRoles]
Delete [dbo].[AspNetUsers]
Delete [dbo].[AspNetUserRoles]
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'02561e7e-ba49-46c1-adb3-b9a6b6130af9', N'admin@mysite.com', 0, N'AHjhwKiiePZKsSeyL21FGF1YBRxoLJrmwtsK0QeUK7lxtXFqcE9uZs4bKvwNqDnYlg==', N'eb4d1343-17fc-4fcb-b6bf-f0fd90371578', NULL, 0, 0, NULL, 1, 0, N'admin@mysite.com')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2aa9bd8e-496b-4141-87fc-4b800bbbbdf1', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'53b3bd7d-0351-4df2-be3b-9f5c1b24c099', N'Developer')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e387a22e-3ce0-4037-a385-dc2615290d08', N'ProjectManager')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'33945cfb-84dd-4873-b641-fd98be6841fb', N'Submitter')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'02561e7e-ba49-46c1-adb3-b9a6b6130af9', N'2aa9bd8e-496b-4141-87fc-4b800bbbbdf1')
");
        }

        public override void Down()
        {
        }
    }
}
