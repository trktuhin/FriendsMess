namespace FriendsMess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class generatingDayNoCollumn : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[DayNoes] ON
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (1, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (2, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (3, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (4, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (5, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (6, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (7, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (8, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (9, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (10, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (11, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (12, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (13, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (14, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (15, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (16, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (17, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (18, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (19, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (20, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (21, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (22, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (23, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (24, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (25, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (26, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (27, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (28, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (29, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (30, 0, 0, NULL)
INSERT INTO [dbo].[DayNoes] ([Id], [TotalMeal], [Expense], [ResponsibleMember]) VALUES (31, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[DayNoes] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}
