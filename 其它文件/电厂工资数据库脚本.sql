USE [master]
GO
/****** Object:  Database [qds16172939_db]    Script Date: 2017/12/4 9:19:40 ******/
CREATE DATABASE [qds16172939_db] ON  PRIMARY 
( NAME = N'qds16172939_db_Data', FILENAME = N'E:\sqldata\qds16172939_db_Data.mdf' , SIZE = 2304KB , MAXSIZE = 51200KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'qds16172939_db_log', FILENAME = N'E:\sqllog\qds16172939_db_log.ldf' , SIZE = 18240KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [qds16172939_db] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [qds16172939_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [qds16172939_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [qds16172939_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [qds16172939_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [qds16172939_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [qds16172939_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [qds16172939_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [qds16172939_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [qds16172939_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [qds16172939_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [qds16172939_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [qds16172939_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [qds16172939_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [qds16172939_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [qds16172939_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [qds16172939_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [qds16172939_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [qds16172939_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [qds16172939_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [qds16172939_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [qds16172939_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [qds16172939_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [qds16172939_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [qds16172939_db] SET RECOVERY FULL 
GO
ALTER DATABASE [qds16172939_db] SET  MULTI_USER 
GO
ALTER DATABASE [qds16172939_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [qds16172939_db] SET DB_CHAINING OFF 
GO
USE [qds16172939_db]
GO
/****** Object:  Table [dbo].[accken_temp]    Script Date: 2017/12/4 9:19:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accken_temp](
	[appid] [nchar](30) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[allmax]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[allmax](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[months] [nchar](6) NULL,
 CONSTRAINT [PK_allmax] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[p_passpass]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[p_passpass](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[员工ID] [bigint] NULL,
	[用户名] [nchar](20) NULL,
	[密码] [nchar](20) NULL,
	[角色ID] [bigint] NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_p_passpass_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_passpass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[p_role]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[p_role](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[角色] [nchar](20) NULL,
	[类型] [decimal](1, 0) NULL CONSTRAINT [DF_p_role_类型]  DEFAULT ((0)),
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_p_role_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_p_role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[permoney]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permoney](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[部门ID] [bigint] NULL,
	[员工ID] [bigint] NULL,
	[条目ID] [bigint] NULL,
	[金额] [decimal](12, 2) NULL CONSTRAINT [DF_permoney_金额]  DEFAULT ((0)),
	[月份] [nchar](6) NULL,
	[操作员] [bigint] NULL,
	[日期] [datetime] NULL CONSTRAINT [DF_permoney_日期]  DEFAULT (CONVERT([varchar](10),getdate(),(120))),
	[排序] [decimal](9, 0) NULL CONSTRAINT [DF_permoney_排序]  DEFAULT ((0)),
 CONSTRAINT [PK_permoney] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[permoney_lr]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permoney_lr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[文字内容] [nchar](100) NULL,
 CONSTRAINT [PK_permoney_lr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[person]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[姓名] [nchar](20) NULL,
	[拼音] [nchar](20) NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_person_删除]  DEFAULT ((0)),
	[微信ID] [nchar](30) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject_mon]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject_mon](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[月份] [nchar](6) NULL,
	[条目ID] [bigint] NULL,
	[排序] [decimal](3, 0) NULL CONSTRAINT [DF_Subject_mon_排序]  DEFAULT ((0)),
	[类型] [decimal](1, 0) NULL CONSTRAINT [DF_Subject_mon_类型]  DEFAULT ((0)),
 CONSTRAINT [PK_Subject_mon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject_name]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject_name](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[条目名称] [nchar](20) NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_Subject_name_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_Subject_name] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[z_fcname]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[z_fcname](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[部门名称] [nchar](50) NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_z_fcname_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_z_fcname] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[z_menu]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[z_menu](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[上级ID] [bigint] NULL CONSTRAINT [DF_z_menu_上级ID]  DEFAULT ((0)),
	[名称] [nchar](50) NULL,
	[表单] [nchar](100) NULL,
	[最大化] [decimal](1, 0) NULL CONSTRAINT [DF_z_menu_最大化]  DEFAULT ((0)),
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_z_menu_删除]  DEFAULT ((0)),
	[月份] [nchar](6) NULL,
 CONSTRAINT [PK_z_menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[View_1]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
WITH CTE1 AS (SELECT   ID, 上级ID, 名称, 表单, 最大化, 删除, 月份, 0 AS 级数
                         FROM      dbo.z_menu
                         WHERE   (上级ID = 0)
                         UNION ALL
                         SELECT   A.ID, A.上级ID, A.名称, A.表单, A.最大化, A.删除, A.月份, B.级数 + 1 AS Expr1
                         FROM      dbo.z_menu AS A INNER JOIN
                                         CTE1 AS B ON A.上级ID = B.ID)
    SELECT   TOP (100) PERCENT ID, 上级ID, 名称, 表单, 最大化, 删除, 月份, 级数,
                        (SELECT   COUNT(1) AS Expr1
                         FROM      dbo.z_menu AS z_menu_1
                         WHERE   (上级ID = CTE1_1.ID)) AS 有下级
    FROM      CTE1 AS CTE1_1
    ORDER BY ID

GO
/****** Object:  View [dbo].[z_menu_view]    Script Date: 2017/12/4 9:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[z_menu_view]
AS
WITH CTE1 AS (SELECT   ID, 上级ID, 名称, 表单, 最大化, 删除, 月份, 0 AS 级数
                         FROM      dbo.z_menu
                         WHERE   (上级ID = 0)
                         UNION ALL
                         SELECT   A.ID, A.上级ID, A.名称, A.表单, A.最大化, A.删除, A.月份, B.级数 + 1 AS Expr1
                         FROM      dbo.z_menu AS A INNER JOIN
                                         CTE1 AS B ON A.上级ID = B.ID)
    SELECT   TOP (100) PERCENT ID, 上级ID, 名称, 表单, 最大化, 删除, 月份, 级数,
                        (SELECT   COUNT(1) AS Expr1
                         FROM      dbo.z_menu AS z_menu_1
                         WHERE   (上级ID = CTE1_1.ID)) AS 有下级
    FROM      CTE1 AS CTE1_1
    ORDER BY ID

GO
ALTER TABLE [dbo].[p_passpass]  WITH CHECK ADD  CONSTRAINT [FK_p_passpass_p_role] FOREIGN KEY([角色ID])
REFERENCES [dbo].[p_role] ([ID])
GO
ALTER TABLE [dbo].[p_passpass] CHECK CONSTRAINT [FK_p_passpass_p_role]
GO
ALTER TABLE [dbo].[p_passpass]  WITH CHECK ADD  CONSTRAINT [FK_p_passpass_person] FOREIGN KEY([员工ID])
REFERENCES [dbo].[person] ([ID])
GO
ALTER TABLE [dbo].[p_passpass] CHECK CONSTRAINT [FK_p_passpass_person]
GO
ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_permoney] FOREIGN KEY([部门ID])
REFERENCES [dbo].[z_fcname] ([ID])
GO
ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_permoney]
GO
ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_person] FOREIGN KEY([员工ID])
REFERENCES [dbo].[person] ([ID])
GO
ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_person]
GO
ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_Subject_name] FOREIGN KEY([条目ID])
REFERENCES [dbo].[Subject_name] ([ID])
GO
ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_Subject_name]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保存工资表中一些文字内的东西' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'permoney_lr', @level2type=N'COLUMN',@level2name=N'文字内容'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'为0表示是金额数字，为1表示为文字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject_mon', @level2type=N'COLUMN',@level2name=N'类型'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CTE1_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CTE1_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'z_menu_view'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'z_menu_view'
GO
USE [master]
GO
ALTER DATABASE [qds16172939_db] SET  READ_WRITE 
GO
