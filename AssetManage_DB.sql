USE [master]
GO
/****** Object:  Database [AssetManage_DB]    Script Date: 2021/4/2 星期五 23:44:41 ******/
CREATE DATABASE [AssetManage_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AssetManage_DB', FILENAME = N'E:\球博资产管理系统\AssetManage_DB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AssetManage_DB_log', FILENAME = N'E:\球博资产管理系统\AssetManage_DB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AssetManage_DB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AssetManage_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AssetManage_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AssetManage_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AssetManage_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AssetManage_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AssetManage_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AssetManage_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AssetManage_DB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AssetManage_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AssetManage_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AssetManage_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AssetManage_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AssetManage_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AssetManage_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AssetManage_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AssetManage_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AssetManage_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AssetManage_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AssetManage_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AssetManage_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AssetManage_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AssetManage_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AssetManage_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AssetManage_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AssetManage_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [AssetManage_DB] SET  MULTI_USER 
GO
ALTER DATABASE [AssetManage_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AssetManage_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AssetManage_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AssetManage_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [AssetManage_DB]
GO
/****** Object:  Table [dbo].[AdminPersonalInfo]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminPersonalInfo](
	[Personal_id] [int] IDENTITY(1,1) NOT NULL,
	[Personal_no] [nvarchar](50) NOT NULL,
	[Personal_depart] [nvarchar](50) NOT NULL,
	[Personal_position] [nvarchar](50) NOT NULL,
	[Personal_name] [nvarchar](50) NOT NULL,
	[Personal_iphone] [nvarchar](50) NULL,
	[Personal_sex] [nvarchar](50) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_AdminPersonalInfo] PRIMARY KEY CLUSTERED 
(
	[Personal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AdminPersonalInfo] UNIQUE NONCLUSTERED 
(
	[Personal_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AssetsClass]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetsClass](
	[AsClass_id] [int] IDENTITY(1,1) NOT NULL,
	[AsClass_no] [nvarchar](50) NOT NULL,
	[AsClass_name] [nvarchar](50) NOT NULL,
	[AsClass_state] [int] NOT NULL,
 CONSTRAINT [PK_AssetsClass] PRIMARY KEY CLUSTERED 
(
	[AsClass_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AssetsClass] UNIQUE NONCLUSTERED 
(
	[AsClass_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AssetsClass_1] UNIQUE NONCLUSTERED 
(
	[AsClass_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BorrowAndReturn]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowAndReturn](
	[bandr_id] [int] IDENTITY(1,1) NOT NULL,
	[bandr_borrowno] [nvarchar](50) NOT NULL,
	[bandr_name] [nvarchar](50) NOT NULL,
	[bandr_borrowtime] [date] NOT NULL,
	[bandr_depart] [int] NOT NULL,
	[bandr_reason] [nvarchar](200) NOT NULL,
	[bandr_returntime] [date] NULL,
	[bandr_wareid] [int] NOT NULL,
	[bandr_remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_BorrowAndReturn] PRIMARY KEY CLUSTERED 
(
	[bandr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_BorrowAndReturn] UNIQUE NONCLUSTERED 
(
	[bandr_borrowno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Brand]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[brand_id] [int] IDENTITY(1,1) NOT NULL,
	[brand_no] [nvarchar](50) NOT NULL,
	[brand_name] [nvarchar](50) NOT NULL,
	[brand_state] [int] NOT NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Brand] UNIQUE NONCLUSTERED 
(
	[brand_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Brand_1] UNIQUE NONCLUSTERED 
(
	[brand_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[depart_id] [int] IDENTITY(1,1) NOT NULL,
	[depart_no] [nvarchar](50) NOT NULL,
	[depart_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[depart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Department] UNIQUE NONCLUSTERED 
(
	[depart_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Department_1] UNIQUE NONCLUSTERED 
(
	[depart_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Scrap]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scrap](
	[scr_id] [int] IDENTITY(1,1) NOT NULL,
	[scr_name] [nvarchar](50) NOT NULL,
	[scr_time] [date] NOT NULL,
	[scr_type] [int] NOT NULL,
	[scr_reason] [nvarchar](200) NOT NULL,
	[scr_wareid] [int] NOT NULL,
 CONSTRAINT [PK_Scrap] PRIMARY KEY CLUSTERED 
(
	[scr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScrapType]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScrapType](
	[scrap_id] [int] IDENTITY(1,1) NOT NULL,
	[scrap_no] [nvarchar](50) NOT NULL,
	[scrap_name] [nvarchar](50) NOT NULL,
	[scrap_state] [int] NOT NULL,
 CONSTRAINT [PK_ScrapType] PRIMARY KEY CLUSTERED 
(
	[scrap_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ScrapType] UNIQUE NONCLUSTERED 
(
	[scrap_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ScrapType_1] UNIQUE NONCLUSTERED 
(
	[scrap_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoragePlace]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoragePlace](
	[place_id] [int] IDENTITY(1,1) NOT NULL,
	[place_name] [nvarchar](50) NOT NULL,
	[place_type] [nvarchar](50) NOT NULL,
	[place_state] [int] NOT NULL,
	[place_remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_StoragePlace] PRIMARY KEY CLUSTERED 
(
	[place_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[sup_id] [int] IDENTITY(1,1) NOT NULL,
	[sup_name] [nvarchar](50) NOT NULL,
	[sup_type] [nvarchar](50) NOT NULL,
	[sup_state] [int] NOT NULL,
	[sup_iphone] [nvarchar](50) NULL,
	[sup_contact] [nvarchar](50) NOT NULL,
	[sup_address] [nvarchar](50) NULL,
 CONSTRAINT [PK_Supplier_1] PRIMARY KEY CLUSTERED 
(
	[sup_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserPrivileg]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrivileg](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[user_pwd] [nvarchar](50) NOT NULL,
	[power] [int] NOT NULL,
 CONSTRAINT [PK_UserPrivileg] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warehous]    Script Date: 2021/4/2 星期五 23:44:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehous](
	[ware_id] [int] IDENTITY(1,1) NOT NULL,
	[ware_no] [nvarchar](50) NULL,
	[ware_name] [nvarchar](50) NOT NULL,
	[ware_class] [int] NOT NULL,
	[ware_sup] [int] NOT NULL,
	[ware_brand] [int] NOT NULL,
	[ware_addtime] [datetime] NULL,
	[ware_place] [int] NOT NULL,
	[ware_brstate] [int] NOT NULL,
	[ware_scrstate] [int] NOT NULL,
 CONSTRAINT [PK_Warehous] PRIMARY KEY CLUSTERED 
(
	[ware_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Warehous] UNIQUE NONCLUSTERED 
(
	[ware_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AssetsClass] ADD  CONSTRAINT [DF_AssetsClass_AsClass_state]  DEFAULT ((1)) FOR [AsClass_state]
GO
ALTER TABLE [dbo].[BorrowAndReturn] ADD  CONSTRAINT [DF_BorrowAndReturn_bandr_borrowtime]  DEFAULT (getdate()) FOR [bandr_borrowtime]
GO
ALTER TABLE [dbo].[Brand] ADD  CONSTRAINT [DF_Brand_brand_state]  DEFAULT ((1)) FOR [brand_state]
GO
ALTER TABLE [dbo].[Scrap] ADD  CONSTRAINT [DF_Scrap_scr_time]  DEFAULT (getdate()) FOR [scr_time]
GO
ALTER TABLE [dbo].[ScrapType] ADD  CONSTRAINT [DF_ScrapType_scrap_state]  DEFAULT ((1)) FOR [scrap_state]
GO
ALTER TABLE [dbo].[StoragePlace] ADD  CONSTRAINT [DF_StoragePlace_place_state]  DEFAULT ((1)) FOR [place_state]
GO
ALTER TABLE [dbo].[Supplier] ADD  CONSTRAINT [DF_Supplier_sup_state]  DEFAULT ((1)) FOR [sup_state]
GO
ALTER TABLE [dbo].[Warehous] ADD  CONSTRAINT [DF_Warehous_ware_addtime]  DEFAULT (getdate()) FOR [ware_addtime]
GO
ALTER TABLE [dbo].[Warehous] ADD  CONSTRAINT [DF_Warehous_ware_brstate]  DEFAULT ((0)) FOR [ware_brstate]
GO
ALTER TABLE [dbo].[Warehous] ADD  CONSTRAINT [DF_Warehous_ware_srcstate]  DEFAULT ((0)) FOR [ware_scrstate]
GO
ALTER TABLE [dbo].[AdminPersonalInfo]  WITH CHECK ADD  CONSTRAINT [FK_AdminPersonalInfo_UserPrivileg] FOREIGN KEY([user_id])
REFERENCES [dbo].[UserPrivileg] ([user_id])
GO
ALTER TABLE [dbo].[AdminPersonalInfo] CHECK CONSTRAINT [FK_AdminPersonalInfo_UserPrivileg]
GO
ALTER TABLE [dbo].[BorrowAndReturn]  WITH CHECK ADD  CONSTRAINT [FK_BorrowAndReturn_Department] FOREIGN KEY([bandr_depart])
REFERENCES [dbo].[Department] ([depart_id])
GO
ALTER TABLE [dbo].[BorrowAndReturn] CHECK CONSTRAINT [FK_BorrowAndReturn_Department]
GO
ALTER TABLE [dbo].[BorrowAndReturn]  WITH CHECK ADD  CONSTRAINT [FK_BorrowAndReturn_Warehous] FOREIGN KEY([bandr_wareid])
REFERENCES [dbo].[Warehous] ([ware_id])
GO
ALTER TABLE [dbo].[BorrowAndReturn] CHECK CONSTRAINT [FK_BorrowAndReturn_Warehous]
GO
ALTER TABLE [dbo].[Scrap]  WITH CHECK ADD  CONSTRAINT [FK_Scrap_ScrapType] FOREIGN KEY([scr_type])
REFERENCES [dbo].[ScrapType] ([scrap_id])
GO
ALTER TABLE [dbo].[Scrap] CHECK CONSTRAINT [FK_Scrap_ScrapType]
GO
ALTER TABLE [dbo].[Scrap]  WITH CHECK ADD  CONSTRAINT [FK_Scrap_Warehous] FOREIGN KEY([scr_wareid])
REFERENCES [dbo].[Warehous] ([ware_id])
GO
ALTER TABLE [dbo].[Scrap] CHECK CONSTRAINT [FK_Scrap_Warehous]
GO
ALTER TABLE [dbo].[Warehous]  WITH CHECK ADD  CONSTRAINT [FK_Warehous_AssetsClass] FOREIGN KEY([ware_class])
REFERENCES [dbo].[AssetsClass] ([AsClass_id])
GO
ALTER TABLE [dbo].[Warehous] CHECK CONSTRAINT [FK_Warehous_AssetsClass]
GO
ALTER TABLE [dbo].[Warehous]  WITH CHECK ADD  CONSTRAINT [FK_Warehous_Brand] FOREIGN KEY([ware_brand])
REFERENCES [dbo].[Brand] ([brand_id])
GO
ALTER TABLE [dbo].[Warehous] CHECK CONSTRAINT [FK_Warehous_Brand]
GO
ALTER TABLE [dbo].[Warehous]  WITH CHECK ADD  CONSTRAINT [FK_Warehous_StoragePlace] FOREIGN KEY([ware_place])
REFERENCES [dbo].[StoragePlace] ([place_id])
GO
ALTER TABLE [dbo].[Warehous] CHECK CONSTRAINT [FK_Warehous_StoragePlace]
GO
ALTER TABLE [dbo].[Warehous]  WITH CHECK ADD  CONSTRAINT [FK_Warehous_Supplier] FOREIGN KEY([ware_sup])
REFERENCES [dbo].[Supplier] ([sup_id])
GO
ALTER TABLE [dbo].[Warehous] CHECK CONSTRAINT [FK_Warehous_Supplier]
GO
USE [master]
GO
ALTER DATABASE [AssetManage_DB] SET  READ_WRITE 
GO
