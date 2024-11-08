USE [master]
GO
/****** Object:  Database [KoiFarmShop]    Script Date: 11/6/2024 8:23:47 AM ******/
CREATE DATABASE [KoiFarmShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KoiFarmShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.KHOI\MSSQL\DATA\KoiFarmShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KoiFarmShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.KHOI\MSSQL\DATA\KoiFarmShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [KoiFarmShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KoiFarmShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KoiFarmShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KoiFarmShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KoiFarmShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KoiFarmShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KoiFarmShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [KoiFarmShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KoiFarmShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KoiFarmShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KoiFarmShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KoiFarmShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KoiFarmShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KoiFarmShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KoiFarmShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KoiFarmShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KoiFarmShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KoiFarmShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KoiFarmShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KoiFarmShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KoiFarmShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KoiFarmShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KoiFarmShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KoiFarmShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KoiFarmShop] SET RECOVERY FULL 
GO
ALTER DATABASE [KoiFarmShop] SET  MULTI_USER 
GO
ALTER DATABASE [KoiFarmShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KoiFarmShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KoiFarmShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KoiFarmShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KoiFarmShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KoiFarmShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'KoiFarmShop', N'ON'
GO
ALTER DATABASE [KoiFarmShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [KoiFarmShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [KoiFarmShopDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [bigint] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Img] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [bigint] NOT NULL,
	[UserID] [bigint] NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[KoiFish_RatingID] [bigint] NOT NULL,
	[KoiFishID] [bigint] NULL,
	[UserID] [bigint] NULL,
	[Rating] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[KoiFish_RatingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KoiFish]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KoiFish](
	[KoiFishID] [bigint] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CategoryID] [bigint] NULL,
	[SizeID] [bigint] NULL,
	[Description] [nvarchar](250) NULL,
	[Dob] [datetime] NULL,
	[Price] [float] NULL,
	[Origin] [nvarchar](50) NULL,
	[Gender] [nvarchar](10) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[KoiFishID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KoiOrder]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KoiOrder](
	[KoiOrderID] [bigint] NOT NULL,
	[CustomerID] [bigint] NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[KoiOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KoiOrderDetail]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KoiOrderDetail](
	[KoiOrderDetailID] [bigint] NOT NULL,
	[KoiOrderID] [bigint] NULL,
	[KoiFishID] [bigint] NULL,
	[TotalPrice] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[KoiOrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[SizeID] [bigint] NOT NULL,
	[SizeInCm] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Is_Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/6/2024 8:23:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [bigint] NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[Gender] [nvarchar](15) NULL,
	[Address] [nvarchar](250) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([KoiFishID])
REFERENCES [dbo].[KoiFish] ([KoiFishID])
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[KoiFish]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[KoiFish]  WITH CHECK ADD FOREIGN KEY([SizeID])
REFERENCES [dbo].[Size] ([SizeID])
GO
ALTER TABLE [dbo].[KoiOrder]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[KoiOrderDetail]  WITH CHECK ADD FOREIGN KEY([KoiFishID])
REFERENCES [dbo].[KoiFish] ([KoiFishID])
GO
ALTER TABLE [dbo].[KoiOrderDetail]  WITH CHECK ADD FOREIGN KEY([KoiOrderID])
REFERENCES [dbo].[KoiOrder] ([KoiOrderID])
GO
USE [master]
GO
ALTER DATABASE [KoiFarmShop] SET  READ_WRITE 
GO
