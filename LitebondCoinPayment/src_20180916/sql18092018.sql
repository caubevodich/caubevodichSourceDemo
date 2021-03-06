USE [master]
GO
/****** Object:  Database [CryptoIO]    Script Date: 18/09/2018 3:13:18 CH ******/
CREATE DATABASE [CryptoIO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CryptoIO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptoIO.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CryptoIO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CryptoIO_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CryptoIO] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CryptoIO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CryptoIO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CryptoIO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CryptoIO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CryptoIO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CryptoIO] SET ARITHABORT OFF 
GO
ALTER DATABASE [CryptoIO] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CryptoIO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CryptoIO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CryptoIO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CryptoIO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CryptoIO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CryptoIO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CryptoIO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CryptoIO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CryptoIO] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CryptoIO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CryptoIO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CryptoIO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CryptoIO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CryptoIO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CryptoIO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CryptoIO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CryptoIO] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CryptoIO] SET  MULTI_USER 
GO
ALTER DATABASE [CryptoIO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CryptoIO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CryptoIO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CryptoIO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CryptoIO] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CryptoIO] SET QUERY_STORE = OFF
GO
USE [CryptoIO]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CryptoIO]
GO
/****** Object:  User [admin]    Script Date: 18/09/2018 3:13:18 CH ******/
CREATE USER [admin] FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[UserName] [nvarchar](256) NULL,
	[IsDeleted] [bit] NULL,
	[Level] [int] NULL,
	[ReferralLink] [nvarchar](255) NULL,
	[ParentId] [nvarchar](128) NULL,
	[Balance] [decimal](18, 8) NULL,
	[VerificationCode] [nvarchar](256) NULL,
	[VerificationTimeToLive] [datetime] NULL,
	[WalletAddress] [nvarchar](256) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[IsDeleted] [bit] NULL,
	[Level] [int] NULL,
	[ReferralLink] [nvarchar](50) NULL,
	[ParentId] [nvarchar](128) NULL,
	[Balance] [decimal](18, 8) NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Balance]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Balance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](100) NULL,
	[TotalBalance] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Balance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Codition]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Codition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Level] [int] NULL,
	[PercentProfitDaily] [float] NULL,
	[Day] [int] NULL,
	[PercentProfitAndCapital] [float] NULL,
	[MinDeposit] [decimal](18, 8) NULL,
	[MaxDeposit] [decimal](18, 8) NULL,
	[Condition] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Codition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [nvarchar](100) NOT NULL,
	[Pwd1] [nvarchar](150) NULL,
	[Pwd2] [nvarchar](150) NULL,
	[FullName] [nvarchar](150) NULL,
	[BtcAddress] [nvarchar](150) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[ParentUser] [nvarchar](100) NULL,
	[IsAdmin] [bit] NULL,
	[Enable] [bit] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDeposit]    Script Date: 18/09/2018 3:13:19 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDeposit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateCreate] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[TotalDay] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[DateEnd] [datetime] NULL,
	[PercentProfitDaily] [float] NULL,
 CONSTRAINT [PK_HistoryDeposit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryGetIncomeBalance]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryGetIncomeBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateGetBalance] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_HistoryGetIncomeBalance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryGetProfitBalance]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryGetProfitBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateGetBalance] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_HistoryGetProfitBalance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryNetworkIncome]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryNetworkIncome](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_HistoryNetworkIncome] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryReceiveIncome]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryReceiveIncome](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[Referral] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_HistoryReceiveIncome] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryReceiveProfit]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryReceiveProfit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[AmountProfit] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_HistoryReceiveProfit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Income]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[TotalAmountIncome] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Income] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncomeDetail]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncomeDetail](
	[Id] [int] NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[Amount] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_IncomeDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginTime] [datetime] NULL,
	[IPAddress] [nvarchar](50) NULL,
	[UserAgent] [nvarchar](500) NULL,
	[UserId] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mega]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mega](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[MegaRoomId] [bigint] NOT NULL,
	[TotalCoins] [decimal](18, 0) NOT NULL,
	[TotalAmounts] [decimal](18, 0) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[ModifiedTime] [datetime] NULL,
	[ResultBall1] [int] NULL,
	[ResultBall2] [int] NULL,
	[ResultBall3] [int] NULL,
	[ResultBallGold] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Mega] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MegaPlay]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MegaPlay](
	[Id] [uniqueidentifier] NOT NULL,
	[CustId] [nvarchar](100) NULL,
	[MegaRoomId] [bigint] NULL,
	[PlayNumber] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[ModifiedTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MegaPlay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MegaRoom]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MegaRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Value] [nvarchar](150) NULL,
	[CreatedTime] [datetime] NULL,
	[ModifiedTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MegaRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profit]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[TotalAmountProfit] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Profit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfitDaily]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfitDaily](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceive] [datetime] NULL,
	[AmountDaily] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[DepositId] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_ProfitDaily] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiveNetworkComission]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiveNetworkComission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[DateReceiveComission] [datetime] NULL,
	[AmountRemain] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReceiveNetworkComission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[From] [nvarchar](150) NULL,
	[To] [nvarchar](150) NULL,
	[TxId] [nvarchar](200) NULL,
	[Amount] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[TotalConfirm] [int] NULL,
	[Type] [bit] NULL,
	[Network] [nvarchar](50) NULL,
	[NetworkFee] [decimal](18, 8) NULL,
	[ExchangeFee] [decimal](18, 8) NULL,
	[Status] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 18/09/2018 3:13:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Symbol] [nvarchar](50) NULL,
	[Network] [nvarchar](50) NULL,
	[AvailableBalance] [decimal](18, 8) NULL,
	[PendingReceivedBalance] [decimal](18, 8) NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[IsDefault] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (1, NULL, NULL, N'leduchuy1411@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'e5db3463-167d-4185-a66e-deb4299c6236', NULL, 0, 0, NULL, 1, 0, N'leduchuy1411@gmail.com', 0, 1, N'leduchuy1411', N'', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (2, NULL, NULL, N'huy567810@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'bafc6f0b-b84e-4a89-8ed9-ba2c8b573e5e', NULL, 0, 0, NULL, 1, 0, N'huy567810@gmail.com', 0, 0, N'huy567810', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (3, NULL, NULL, N'bao123@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'03908d51-ad92-4293-9cd5-bf43036b0aee', NULL, 0, 0, NULL, 1, 0, N'bao123@gmail.com', 0, 0, N'bao123', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (4, NULL, NULL, N'abc23535@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'0c8dd0c1-0d32-42b6-9d04-52e58cd52603', NULL, 0, 0, NULL, 1, 0, N'abc23535@gmail.com', 0, 0, N'abc23535', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (5, NULL, NULL, N'test1@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'028e244e-3179-4f74-bc66-fb3ffd90f1f2', NULL, 0, 0, NULL, 1, 0, N'test1@gmail.com', 0, 0, N'test1', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (6, N'Le Thanh Tuan', N'/Uploads/Avatars/thumb_20162803155047012ba7ac4f1d40e383c951a9dd4e4a94.jpg', N'tuanitpro@live.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'ee10cdc9-0041-4a3c-a93b-078d6330da08', NULL, 0, 0, NULL, 0, 0, N'tuanitpro@live.com', 0, 0, NULL, N'test1@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (7, N'Thanh Tuan Le', N'/content/images/no-avatar.png', N'tinhoctredalat@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'e81b3e3c-173b-4284-98c9-07f8d1a59832', NULL, 0, 0, NULL, 1, 1, N'tinhoctredalat@gmail.com', 0, 0, NULL, N'tuanitpro@live.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (8, NULL, NULL, N'bao23@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'ad867592-d107-431a-8211-8cb314675bb7', NULL, 0, 0, NULL, 1, 0, N'bao23@gmail.com', 0, 0, N'bao23', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (9, NULL, NULL, N'huy343@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'bc3656c0-e514-43bc-93e0-ec51acbfb41f', NULL, 0, 0, NULL, 1, 0, N'huy343@gmail.com', 0, 0, N'huy343', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (10, NULL, NULL, N'demo1234@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'4c4bf08f-9992-438f-ba40-4b0f602d0de2', NULL, 0, 0, NULL, 1, 0, N'demo1234@gmail.com', 0, 0, N'demo1234', N'tinhoctredalat@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (11, N'Lê Thanh Tuấn', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro23@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro23@gmail.com', 0, 0, NULL, N'tuanitpro@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (12, N'Lê Thanh Tuấn', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro12@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro12@gmail.com', 0, 0, NULL, N'tuanitpro@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (13, N'Lê Thanh Tuấn 123', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432 456', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro@gmail.com', 0, 0, N'tuanitpro', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (14, NULL, NULL, N'huy34234234@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'6598c974-cae3-48a5-99eb-c71a59e40eeb', NULL, 0, 0, NULL, 1, 0, N'huy34234234@gmail.com', 0, 0, N'huy34234234', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (15, NULL, NULL, N'bam23@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', N'cb3224c9-8a8c-40d2-ad13-1146a289dace', NULL, 0, 0, NULL, 1, 0, N'bam23@gmail.com', 0, 0, N'bam23', N'caubevodich9999@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL, NULL, NULL)
GO
INSERT [dbo].[Account] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [VerificationCode], [VerificationTimeToLive], [WalletAddress]) VALUES (18, N'caubevodich', NULL, N'caubevodich9999@gmail.com', 1, N'8BB0CF6EB9B17D0F7D22B456F121257DC1254E1F01665370476383EA776DF414', NULL, N'0909036977', 0, 1, NULL, 0, NULL, N'caubevodich9999@gmail.com', 0, 0, N'caubevodich9999', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), N'83629F4F1D57DB42CEFE133677AED76F4765D35F', CAST(N'2018-09-19T19:10:42.827' AS DateTime), N'QbBNvRfbgPzLK4t1BbgpE7prVCcZdiTKcA7333333333')
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8f7322b6-6773-415e-abb7-1cd92e2766ac', N'Administrator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b78229d7-c220-46a6-b8f6-9927a098f84b', N'Editor')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'148bb4d9-2a07-42d6-b96f-dca7c5d51683', N'Manager')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'02d1564f-1737-40da-953a-9a3d3d27345d', N'8f7322b6-6773-415e-abb7-1cd92e2766ac')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'91ed7402-05e1-4c2b-bdec-2eea7f4d404b', N'b78229d7-c220-46a6-b8f6-9927a098f84b')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cfb38193-d6e4-4e47-b03a-034f5b1549e1', N'8f7322b6-6773-415e-abb7-1cd92e2766ac')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'00f809c3-e345-4e1e-82e4-717efcd960b6', NULL, NULL, N'huyle@gmail.com', 1, N'ADGIWPFk43huFkoqD7ioaWTxGWrUeCb/M5YQNZaUSsUSKaOOKFDv9Xea4iv7Ak7tog==', N'8e41923f-8d85-4f56-8e95-b93b2058aad8', NULL, 0, 0, NULL, 1, 0, N'huyle@gmail.com', 0, 0, N'huyle', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'02d1564f-1737-40da-953a-9a3d3d27345d', NULL, NULL, N'leduchuy1411@gmail.com', 1, N'ANCr+13hGl6PWLSrf7iA9x0VG9X/vgPw9SWEiF2Zd07Du7kZ3W0eoxNfo6fMLPj7dg==', N'e5db3463-167d-4185-a66e-deb4299c6236', NULL, 0, 0, NULL, 1, 0, N'leduchuy1411@gmail.com', 0, 0, N'leduchuy1411', N'', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'1ad1c48e-786a-41f7-8a57-e35d108eb0ef', NULL, NULL, N'huy567810@gmail.com', 1, N'AO6+crWE4jLemu+a/5yLLIj2X2yezBH9gUdjJOTGJnRlRitdkd4kCPt5eLeiDc9v8Q==', N'bafc6f0b-b84e-4a89-8ed9-ba2c8b573e5e', NULL, 0, 0, NULL, 1, 0, N'huy567810@gmail.com', 0, 0, N'huy567810', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'1cf00dda-029e-43e3-8537-0a17117e42dd', NULL, NULL, N'bao123@gmail.com', 1, N'ANTzGLtLnpyRL5ABfbJaxEaOuQrExUVzyfE4K85z5OcI8A3obx+0K67h0JQEhaPQmg==', N'03908d51-ad92-4293-9cd5-bf43036b0aee', NULL, 0, 0, NULL, 1, 0, N'bao123@gmail.com', 0, 0, N'bao123', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'56924f0b-e621-49cb-8e38-a3ccb4cc7795', NULL, NULL, N'huy123@gmail.com', 1, N'AOqRKllytSbOf9bGb16ETpGtsKFN5EtQN+ZrU4tgapsUs3gzX0/h9aWEIbNFIsojXw==', N'691ef7ce-1e19-48ac-9e03-7535e9b1eb4a', NULL, 0, 0, NULL, 1, 0, N'huy123@gmail.com', 0, 0, N'huy123', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'5806254e-0faa-4c73-b30a-5ce4143a1b7c', NULL, NULL, N'abc23535@gmail.com', 1, N'ALMpu25HRVQQ0jpEo+n/FdV4x0ZvdTjMlDwE3zaWfqgN5Lvd2JM0SsZqHNjAPbCJKg==', N'0c8dd0c1-0d32-42b6-9d04-52e58cd52603', NULL, 0, 0, NULL, 1, 0, N'abc23535@gmail.com', 0, 0, N'abc23535', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'8ec63704-272e-4bbd-a382-e2f90c2064bd', NULL, NULL, N'test1@gmail.com', 1, N'AHf2ekzCryW+Tp9Ef23z8YOOWBr5lSlH+dFg6hIbtnD1kw31zmekc5ahqzE+7U/11g==', N'028e244e-3179-4f74-bc66-fb3ffd90f1f2', NULL, 0, 0, NULL, 1, 0, N'test1@gmail.com', 0, 0, N'test1', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'91ed7402-05e1-4c2b-bdec-2eea7f4d404b', N'Le Thanh Tuan', N'/Uploads/Avatars/thumb_20162803155047012ba7ac4f1d40e383c951a9dd4e4a94.jpg', N'tuanitpro@live.com', 1, N'AOrR03UFcAnWGeSNRceLziSuNNmjypub3LjHROPd/YZcKw+iXsr5WJ63+u2cWvu6dQ==', N'ee10cdc9-0041-4a3c-a93b-078d6330da08', NULL, 0, 0, NULL, 0, 0, N'tuanitpro@live.com', 0, 0, NULL, N'test1@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'a04b2599-0102-4c0f-b0cb-63e3d31dda5b', N'Thanh Tuan Le', N'/content/images/no-avatar.png', N'tinhoctredalat@gmail.com', 1, N'AJA8pW5uDJZlrMuZEEtaQTzc09RWdA2viKfUs4qS7yX4ReVIUj1eYisgwBTFO6dSqg==', N'e81b3e3c-173b-4284-98c9-07f8d1a59832', NULL, 0, 0, NULL, 1, 1, N'tinhoctredalat@gmail.com', 0, 0, NULL, N'tuanitpro@live.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'a0b7b229-7a00-4973-a049-a473d3645541', NULL, NULL, N'bao23@gmail.com', 1, N'AEUP7XXxLnBUmMncYs4JrbKePWbjFz1rfAIIjOJ5gblLsmyBmR6Af4kYMtshrBY0AQ==', N'ad867592-d107-431a-8211-8cb314675bb7', NULL, 0, 0, NULL, 1, 0, N'bao23@gmail.com', 0, 0, N'bao23', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'b8abe7de-8758-41b6-bd64-6bd1fe8a9e7a', NULL, NULL, N'huy343@gmail.com', 1, N'AEL4VWNesYUIZrDy53I2Mu2AIRxRyGfmR2zUkZAQBjNcQmur9BrGnSO/OOvdoi2rsg==', N'bc3656c0-e514-43bc-93e0-ec51acbfb41f', NULL, 0, 0, NULL, 1, 0, N'huy343@gmail.com', 0, 0, N'huy343', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'c88cfbfd-1262-43e3-b7b4-6787a3d757bd', NULL, NULL, N'demo1234@gmail.com', 1, N'AIhEacP7S08Q6xeIL6ioUwRTN+lnGTg5klb/u/9OrXUGUWih4OM3Pwv95KA69Ht5bw==', N'4c4bf08f-9992-438f-ba40-4b0f602d0de2', NULL, 0, 0, NULL, 1, 0, N'demo1234@gmail.com', 0, 0, N'demo1234', N'tinhoctredalat@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'cfb38193-d6e4-4e47-b03a-034f5b15239e1', N'Lê Thanh Tuấn', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro23@gmail.com', 1, N'ABxighUyTqY1XF5e8ttECROVmvqs6W3Cb4HncJqgL7sOFgFgatFG/qQ93bzfyqQPiw==', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro23@gmail.com', 0, 0, NULL, N'tuanitpro@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'cfb38193-d6e4-4e47-b03a-034f5b1539e1', N'Lê Thanh Tuấn', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro12@gmail.com', 1, N'ABxighUyTqY1XF5e8ttECROVmvqs6W3Cb4HncJqgL7sOFgFgatFG/qQ93bzfyqQPiw==', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro12@gmail.com', 0, 0, NULL, N'tuanitpro@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'cfb38193-d6e4-4e47-b03a-034f5b1549e1', N'Lê Thanh Tuấn', N'/Content/Avatars/thumb_2016171217130227ce040871b94c58a04d700cbb9d2148.jpg', N'tuanitpro@gmail.com', 1, N'ABxighUyTqY1XF5e8ttECROVmvqs6W3Cb4HncJqgL7sOFgFgatFG/qQ93bzfyqQPiw==', N'ed34c73d-d50a-4a4f-b723-7e4aa0149b93', N'+84976060432', 1, 0, CAST(N'2016-05-05T05:48:46.027' AS DateTime), 0, 0, N'tuanitpro@gmail.com', 0, 0, N'tuanitpro', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'd5adaae4-3867-4a6c-8677-19ebea14d6a5', NULL, NULL, N'huy34234234@gmail.com', 1, N'APU4A6iDi/vYMQcfJEQqePi7HHeU7lMRczG8HxkaKkMdnZzEPQRimC4+q/E3xw83VA==', N'6598c974-cae3-48a5-99eb-c71a59e40eeb', NULL, 0, 0, NULL, 1, 0, N'huy34234234@gmail.com', 0, 0, N'huy34234234', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Avatar], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsDeleted], [Level], [ReferralLink], [ParentId], [Balance], [Address]) VALUES (N'eca73334-a2ed-41a5-9ff4-d940835be4ff', NULL, NULL, N'bam23@gmail.com', 1, N'AHeOWp+tkGOjSBR+atsJdMk/a+gcoiM0Cv0d4EPZnKvJkJpjrT1du8F0/y+rEhRlrg==', N'cb3224c9-8a8c-40d2-ad13-1146a289dace', NULL, 0, 0, NULL, 1, 0, N'bam23@gmail.com', 0, 0, N'bam23', N'leduchuy1411@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), NULL)
GO
SET IDENTITY_INSERT [dbo].[Balance] ON 
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1, N'bao23@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-14T06:50:35.277' AS DateTime), CAST(N'2018-09-14T06:50:35.280' AS DateTime), 0, N'bao23@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'leduchuy1411@gmail.com', CAST(3.85000000 AS Decimal(18, 8)), CAST(N'2018-09-14T10:22:27.607' AS DateTime), CAST(N'2018-09-18T08:00:25.530' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, N'bao123@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:31:14.510' AS DateTime), CAST(N'2018-09-15T15:31:14.510' AS DateTime), 0, N'bao123@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, N'abc23535@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:37:01.900' AS DateTime), CAST(N'2018-09-15T15:37:01.900' AS DateTime), 0, N'abc23535@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, N'huy343@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:07:06.517' AS DateTime), CAST(N'2018-09-15T19:07:06.517' AS DateTime), 0, N'huy343@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, N'huy34234234@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:08:12.407' AS DateTime), CAST(N'2018-09-15T19:08:12.407' AS DateTime), 0, N'huy34234234@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, N'huyle@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:11:10.233' AS DateTime), CAST(N'2018-09-15T19:11:10.233' AS DateTime), 0, N'huyle@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, N'huy123@gmail.com', CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:44:02.013' AS DateTime), CAST(N'2018-09-15T19:44:02.013' AS DateTime), 0, N'huy123@gmail.com')
GO
INSERT [dbo].[Balance] ([Id], [UserId], [TotalBalance], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, N'caubevodich9999@gmail.com', CAST(3.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:44:02.013' AS DateTime), CAST(N'2018-09-15T19:44:02.013' AS DateTime), 0, N'caubevodich9999@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Balance] OFF
GO
SET IDENTITY_INSERT [dbo].[Codition] ON 
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1, 0, 0.6, 260, 156, CAST(0.50000000 AS Decimal(18, 8)), CAST(1.50000000 AS Decimal(18, 8)), 0, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, 1, 0.65, 245, 159.25, CAST(1.50000000 AS Decimal(18, 8)), CAST(3.00000000 AS Decimal(18, 8)), 5, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, 2, 0.7, 235, 164.5, CAST(3.00000000 AS Decimal(18, 8)), CAST(6.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, 3, 0.7, 240, 168, CAST(6.00000000 AS Decimal(18, 8)), CAST(12.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, 4, 0.75, 230, 172.5, CAST(12.00000000 AS Decimal(18, 8)), CAST(24.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, 5, 0.75, 233, 174.75, CAST(24.00000000 AS Decimal(18, 8)), CAST(48.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, 6, 0.8, 225, 180, CAST(48.00000000 AS Decimal(18, 8)), CAST(96.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, 7, 0.8, 227, 181.6, CAST(96.00000000 AS Decimal(18, 8)), CAST(192.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, 8, 0.85, 222, 188.7, CAST(192.00000000 AS Decimal(18, 8)), CAST(384.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (10, 9, 0.9, 221, 198.9, CAST(384.00000000 AS Decimal(18, 8)), CAST(768.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Codition] ([Id], [Level], [PercentProfitDaily], [Day], [PercentProfitAndCapital], [MinDeposit], [MaxDeposit], [Condition], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (11, 10, 0.95, 200, 190, CAST(768.00000000 AS Decimal(18, 8)), CAST(1536.00000000 AS Decimal(18, 8)), 3, CAST(N'2018-09-06T15:37:23.327' AS DateTime), CAST(N'2018-09-06T15:37:23.327' AS DateTime), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Codition] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryDeposit] ON 
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1042, N'leduchuy1411@gmail.com', CAST(N'2018-09-11T15:47:41.463' AS DateTime), CAST(1.50000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-11T15:47:41.463' AS DateTime), CAST(N'2018-09-11T15:47:41.463' AS DateTime), 0, N'leduchuy1411@gmail.com', 1, CAST(N'2019-05-14T15:47:41.463' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1043, N'leduchuy1411@gmail.com', CAST(N'2018-09-11T15:47:56.783' AS DateTime), CAST(0.10000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-11T15:47:56.783' AS DateTime), CAST(N'2018-09-11T15:47:56.783' AS DateTime), 0, N'leduchuy1411@gmail.com', 1, CAST(N'2019-05-14T15:47:56.783' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1044, N'abc23535@gmail.com', CAST(N'2018-09-11T15:47:56.783' AS DateTime), CAST(23.00000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-11T15:47:56.783' AS DateTime), CAST(N'2018-09-11T15:47:56.783' AS DateTime), 0, N'abc23535@gmail.com', 1, CAST(N'2019-05-14T15:47:56.783' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1045, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T08:24:33.133' AS DateTime), CAST(0.30000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-15T08:24:33.133' AS DateTime), CAST(N'2018-09-15T08:24:33.133' AS DateTime), 0, N'leduchuy1411@gmail.com', 1, CAST(N'2019-05-18T08:24:33.133' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1046, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T08:29:16.203' AS DateTime), CAST(0.60000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-15T08:29:16.203' AS DateTime), CAST(N'2018-09-15T08:29:16.203' AS DateTime), 0, N'leduchuy1411@gmail.com', 1, CAST(N'2019-05-18T08:29:16.203' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1047, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T08:34:25.980' AS DateTime), CAST(0.20000000 AS Decimal(18, 8)), 245, CAST(N'2018-09-15T08:34:25.980' AS DateTime), CAST(N'2018-09-15T08:34:25.980' AS DateTime), 0, N'leduchuy1411@gmail.com', 1, CAST(N'2019-05-18T08:34:25.980' AS DateTime), 0.65)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1048, N'caubevodich9999@gmail.com', CAST(N'2018-09-16T17:59:19.603' AS DateTime), CAST(0.50000000 AS Decimal(18, 8)), 260, CAST(N'2018-09-16T17:59:19.603' AS DateTime), CAST(N'2018-09-16T17:59:19.603' AS DateTime), 0, N'caubevodich9999@gmail.com', 1, CAST(N'2019-06-03T17:59:19.603' AS DateTime), 0.6)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1049, N'caubevodich9999@gmail.com', CAST(N'2018-09-16T18:07:28.120' AS DateTime), CAST(0.50000000 AS Decimal(18, 8)), 260, CAST(N'2018-09-16T18:07:28.120' AS DateTime), CAST(N'2018-09-16T18:07:28.120' AS DateTime), 0, N'caubevodich9999@gmail.com', 1, CAST(N'2019-06-03T18:07:28.120' AS DateTime), 0.6)
GO
INSERT [dbo].[HistoryDeposit] ([Id], [UserId], [DateCreate], [Amount], [TotalDay], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status], [DateEnd], [PercentProfitDaily]) VALUES (1050, N'caubevodich9999@gmail.com', CAST(N'2018-09-16T18:18:30.563' AS DateTime), CAST(0.50000000 AS Decimal(18, 8)), 260, CAST(N'2018-09-16T18:18:30.563' AS DateTime), CAST(N'2018-09-16T18:18:30.563' AS DateTime), 0, N'caubevodich9999@gmail.com', 1, CAST(N'2019-06-03T18:18:30.563' AS DateTime), 0.6)
GO
SET IDENTITY_INSERT [dbo].[HistoryDeposit] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryGetIncomeBalance] ON 
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1, N'leduchuy1411@gmail.com', CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(0.34300000 AS Decimal(18, 8)), CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(N'2018-07-21T15:37:23.327' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'leduchuy1411@gmail.com', CAST(N'2018-09-12T07:08:23.287' AS DateTime), CAST(35.14500000 AS Decimal(18, 8)), CAST(N'2018-09-12T07:08:23.287' AS DateTime), CAST(N'2018-09-12T07:08:23.287' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, N'leduchuy1411@gmail.com', CAST(N'2018-09-12T07:13:15.593' AS DateTime), CAST(0.20000000 AS Decimal(18, 8)), CAST(N'2018-09-12T07:13:15.593' AS DateTime), CAST(N'2018-09-12T07:13:15.593' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, N'leduchuy1411@gmail.com', CAST(N'2018-09-12T07:16:00.913' AS DateTime), CAST(0.10000000 AS Decimal(18, 8)), CAST(N'2018-09-12T07:16:00.913' AS DateTime), CAST(N'2018-09-12T07:16:00.913' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:17:28.133' AS DateTime), CAST(0.15000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:17:28.133' AS DateTime), CAST(N'2018-09-13T03:17:28.133' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:18:59.783' AS DateTime), CAST(0.15000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:18:59.783' AS DateTime), CAST(N'2018-09-13T03:18:59.783' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:21:51.853' AS DateTime), CAST(0.15000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:21:51.853' AS DateTime), CAST(N'2018-09-13T03:21:51.853' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:28:19.327' AS DateTime), CAST(0.15000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:28:19.327' AS DateTime), CAST(N'2018-09-13T03:28:19.327' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:28:30.983' AS DateTime), CAST(0.00500000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:28:30.983' AS DateTime), CAST(N'2018-09-13T03:28:30.983' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (10, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:58:29.783' AS DateTime), CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:58:29.783' AS DateTime), CAST(N'2018-09-13T03:58:29.783' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (11, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:59:25.663' AS DateTime), CAST(0.00050000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:59:25.663' AS DateTime), CAST(N'2018-09-13T03:59:25.663' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (12, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T17:55:35.747' AS DateTime), CAST(0.00500000 AS Decimal(18, 8)), CAST(N'2018-09-13T17:55:35.747' AS DateTime), CAST(N'2018-09-13T17:55:35.747' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (13, N'leduchuy1411@gmail.com', CAST(N'2018-09-14T12:51:13.143' AS DateTime), CAST(0.00800000 AS Decimal(18, 8)), CAST(N'2018-09-14T12:51:13.143' AS DateTime), CAST(N'2018-09-14T12:51:13.143' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (14, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T09:02:31.830' AS DateTime), CAST(0.01000000 AS Decimal(18, 8)), CAST(N'2018-09-15T09:02:31.830' AS DateTime), CAST(N'2018-09-15T09:02:31.830' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (15, N'leduchuy1411@gmail.com', CAST(N'2018-09-16T18:02:35.723' AS DateTime), CAST(0.22400000 AS Decimal(18, 8)), CAST(N'2018-09-16T18:02:35.723' AS DateTime), CAST(N'2018-09-16T18:02:35.723' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetIncomeBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (16, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T03:20:03.590' AS DateTime), CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-18T03:20:03.590' AS DateTime), CAST(N'2018-09-18T03:20:03.590' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[HistoryGetIncomeBalance] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryGetProfitBalance] ON 
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1, N'leduchuy1411@gmail.com', CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(0.34300000 AS Decimal(18, 8)), CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(N'2018-07-21T15:37:23.327' AS DateTime), 0, NULL)
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:23:27.527' AS DateTime), CAST(0.21000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:23:27.527' AS DateTime), CAST(N'2018-09-13T03:23:27.527' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T03:24:40.983' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-13T03:24:40.983' AS DateTime), CAST(N'2018-09-13T03:24:40.983' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T18:09:20.830' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-13T18:09:20.830' AS DateTime), CAST(N'2018-09-13T18:09:20.830' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T06:51:35.870' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-15T06:51:35.870' AS DateTime), CAST(N'2018-09-15T06:51:35.870' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T07:34:06.157' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-15T07:34:06.157' AS DateTime), CAST(N'2018-09-15T07:34:06.157' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T08:24:21.807' AS DateTime), CAST(0.02080000 AS Decimal(18, 8)), CAST(N'2018-09-15T08:24:21.807' AS DateTime), CAST(N'2018-09-15T08:24:21.807' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T03:13:56.787' AS DateTime), CAST(0.01000000 AS Decimal(18, 8)), CAST(N'2018-09-18T03:13:56.787' AS DateTime), CAST(N'2018-09-18T03:13:56.787' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T03:14:45.590' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-18T03:14:45.590' AS DateTime), CAST(N'2018-09-18T03:14:45.590' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[HistoryGetProfitBalance] ([Id], [UserId], [DateGetBalance], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (10, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T03:19:43.360' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-18T03:19:43.360' AS DateTime), CAST(N'2018-09-18T03:19:43.360' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[HistoryGetProfitBalance] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryNetworkIncome] ON 
GO
INSERT [dbo].[HistoryNetworkIncome] ([Id], [UserId], [DateReceive], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (1024, N'leduchuy1411@gmail.com', CAST(N'2018-09-14T13:01:24.477' AS DateTime), CAST(0.11200000 AS Decimal(18, 8)), CAST(N'2018-09-14T13:01:24.477' AS DateTime), CAST(N'2018-09-16T18:02:26.250' AS DateTime), 0, N'leduchuy1411@gmail.com', 1)
GO
INSERT [dbo].[HistoryNetworkIncome] ([Id], [UserId], [DateReceive], [Amount], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (1025, N'leduchuy1411@gmail.com', CAST(N'2018-09-14T13:06:00.543' AS DateTime), CAST(0.11200000 AS Decimal(18, 8)), CAST(N'2018-09-14T13:06:00.543' AS DateTime), CAST(N'2018-09-16T18:02:28.393' AS DateTime), 0, N'leduchuy1411@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[HistoryNetworkIncome] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryReceiveIncome] ON 
GO
INSERT [dbo].[HistoryReceiveIncome] ([Id], [UserId], [DateReceive], [Amount], [Referral], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (1, N'caubevodich9999@gmail.com', CAST(N'2018-09-16T17:59:19.627' AS DateTime), CAST(0.05000000 AS Decimal(18, 8)), N'leduchuy1411@gmail.com', CAST(N'2018-09-16T17:59:19.627' AS DateTime), CAST(N'2018-09-17T06:57:26.693' AS DateTime), 0, N'caubevodich9999@gmail.com', 1)
GO
INSERT [dbo].[HistoryReceiveIncome] ([Id], [UserId], [DateReceive], [Amount], [Referral], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (2, N'caubevodich9999@gmail.com', CAST(N'2018-09-16T18:07:28.127' AS DateTime), CAST(0.05000000 AS Decimal(18, 8)), N'leduchuy1411@gmail.com', CAST(N'2018-09-16T18:07:28.127' AS DateTime), CAST(N'2018-09-17T06:57:33.547' AS DateTime), 0, N'caubevodich9999@gmail.com', 1)
GO
INSERT [dbo].[HistoryReceiveIncome] ([Id], [UserId], [DateReceive], [Amount], [Referral], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (3, N'leduchuy1411@gmail.com', CAST(N'2018-09-16T18:18:30.767' AS DateTime), CAST(0.05000000 AS Decimal(18, 8)), N'caubevodich9999@gmail.com', CAST(N'2018-09-16T18:18:30.767' AS DateTime), CAST(N'2018-09-17T16:34:48.477' AS DateTime), 0, N'leduchuy1411@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[HistoryReceiveIncome] OFF
GO
SET IDENTITY_INSERT [dbo].[HistoryReceiveProfit] ON 
GO
INSERT [dbo].[HistoryReceiveProfit] ([Id], [UserId], [DateReceive], [AmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [Status]) VALUES (1, N'leduchuy1411@gmail.com', CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(0.34300000 AS Decimal(18, 8)), CAST(N'2018-07-21T15:37:23.327' AS DateTime), CAST(N'2018-07-21T15:37:23.327' AS DateTime), 0, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[HistoryReceiveProfit] OFF
GO
SET IDENTITY_INSERT [dbo].[Income] ON 
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'leduchuy1411@gmail.com', CAST(N'2018-09-06T03:11:31.750' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-05T20:11:31.750' AS DateTime), CAST(N'2018-09-18T03:20:03.587' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, N'abc23535@gmail.com', CAST(N'2018-09-11T18:42:54.547' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-11T18:42:54.547' AS DateTime), CAST(N'2018-09-11T18:42:54.547' AS DateTime), 0, N'abc23535@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, N'huy567810@gmail.com', CAST(N'2018-09-12T16:01:44.043' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-12T16:01:44.043' AS DateTime), CAST(N'2018-09-12T16:01:44.043' AS DateTime), 0, N'huy567810@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, N'bao23@gmail.com', CAST(N'2018-09-14T06:50:35.393' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-14T06:50:35.393' AS DateTime), CAST(N'2018-09-14T06:50:35.393' AS DateTime), 0, N'bao23@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, N'bao123@gmail.com', CAST(N'2018-09-15T15:31:14.617' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:31:14.617' AS DateTime), CAST(N'2018-09-15T15:31:14.617' AS DateTime), 0, N'bao123@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, N'huy343@gmail.com', CAST(N'2018-09-15T19:07:06.550' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:07:06.550' AS DateTime), CAST(N'2018-09-15T19:07:06.550' AS DateTime), 0, N'huy343@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, N'huy34234234@gmail.com', CAST(N'2018-09-15T19:08:12.440' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:08:12.440' AS DateTime), CAST(N'2018-09-15T19:08:12.440' AS DateTime), 0, N'huy34234234@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, N'huyle@gmail.com', CAST(N'2018-09-15T19:11:10.243' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:11:10.243' AS DateTime), CAST(N'2018-09-15T19:11:10.243' AS DateTime), 0, N'huyle@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (10, N'huy123@gmail.com', CAST(N'2018-09-15T19:44:02.853' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:44:02.853' AS DateTime), CAST(N'2018-09-15T19:44:02.853' AS DateTime), 0, N'huy123@gmail.com')
GO
INSERT [dbo].[Income] ([Id], [UserId], [DateReceive], [TotalAmountIncome], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (11, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T07:06:00.217' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:06:00.217' AS DateTime), CAST(N'2018-09-17T07:06:00.217' AS DateTime), 0, N'caubevodich9999@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Income] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] ON 
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (39, CAST(N'2018-09-17T03:20:30.950' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (40, CAST(N'2018-09-17T04:08:11.797' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (41, CAST(N'2018-09-17T04:20:21.067' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (42, CAST(N'2018-09-17T04:22:48.387' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (43, CAST(N'2018-09-17T04:56:19.420' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (44, CAST(N'2018-09-17T05:27:46.443' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (45, CAST(N'2018-09-17T05:29:51.003' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (46, CAST(N'2018-09-17T05:45:02.847' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (47, CAST(N'2018-09-17T06:18:51.640' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (48, CAST(N'2018-09-17T06:49:40.810' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (49, CAST(N'2018-09-17T07:08:31.533' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (50, CAST(N'2018-09-17T07:15:07.053' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (51, CAST(N'2018-09-17T08:22:04.540' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (52, CAST(N'2018-09-17T09:04:29.723' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (53, CAST(N'2018-09-17T12:13:35.310' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (54, CAST(N'2018-09-17T12:54:56.857' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (55, CAST(N'2018-09-17T13:01:25.697' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 18, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (56, CAST(N'2018-09-17T13:41:55.953' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (57, CAST(N'2018-09-17T13:52:32.380' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (58, CAST(N'2018-09-17T14:24:37.713' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (59, CAST(N'2018-09-17T14:56:49.913' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (60, CAST(N'2018-09-17T15:33:35.667' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (61, CAST(N'2018-09-17T16:06:06.737' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (62, CAST(N'2018-09-17T16:36:16.957' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (63, CAST(N'2018-09-17T17:23:59.993' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', -1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (64, CAST(N'2018-09-17T18:14:58.760' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (65, CAST(N'2018-09-17T18:45:45.047' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (66, CAST(N'2018-09-17T19:22:51.730' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 18, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (67, CAST(N'2018-09-17T19:23:43.740' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (68, CAST(N'2018-09-17T19:54:22.973' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (69, CAST(N'2018-09-18T02:43:15.417' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (70, CAST(N'2018-09-18T03:13:34.750' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (71, CAST(N'2018-09-18T03:54:16.720' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (72, CAST(N'2018-09-18T04:18:43.593' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (73, CAST(N'2018-09-18T04:51:15.080' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (74, CAST(N'2018-09-18T05:35:40.987' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (75, CAST(N'2018-09-18T06:06:43.650' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (76, CAST(N'2018-09-18T07:12:54.813' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
INSERT [dbo].[LoginHistory] ([Id], [LoginTime], [IPAddress], [UserAgent], [UserId], [IsDeleted]) VALUES (77, CAST(N'2018-09-18T07:45:21.107' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[LoginHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[MegaRoom] ON 
GO
INSERT [dbo].[MegaRoom] ([Id], [Name], [Value], [CreatedTime], [ModifiedTime], [IsDeleted]) VALUES (1, N'Name', N'Value', CAST(N'2016-10-10T00:00:00.000' AS DateTime), CAST(N'2016-10-10T00:00:00.000' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[MegaRoom] OFF
GO
SET IDENTITY_INSERT [dbo].[Profit] ON 
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'leduchuy1411@gmail.com', CAST(N'2018-09-07T13:41:10.923' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-07T06:41:10.923' AS DateTime), CAST(N'2018-09-18T03:19:43.337' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1002, N'abc23535@gmail.com', CAST(N'2018-09-11T18:42:50.257' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-11T18:42:50.257' AS DateTime), CAST(N'2018-09-11T18:42:50.257' AS DateTime), 0, N'abc23535@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1003, N'huy343@gmail.com', CAST(N'2018-09-12T07:27:29.630' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-12T07:27:29.630' AS DateTime), CAST(N'2018-09-12T07:27:29.630' AS DateTime), 0, N'huy343@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1004, N'huy567810@gmail.com', CAST(N'2018-09-12T16:01:44.007' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-12T16:01:44.007' AS DateTime), CAST(N'2018-09-12T16:01:44.007' AS DateTime), 0, N'huy567810@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1005, N'bao23@gmail.com', CAST(N'2018-09-14T06:50:35.370' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-14T06:50:35.370' AS DateTime), CAST(N'2018-09-14T06:50:35.370' AS DateTime), 0, N'bao23@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1006, N'bao123@gmail.com', CAST(N'2018-09-15T15:31:14.610' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:31:14.610' AS DateTime), CAST(N'2018-09-15T15:31:14.610' AS DateTime), 0, N'bao123@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1007, N'huy34234234@gmail.com', CAST(N'2018-09-15T19:08:12.410' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:08:12.410' AS DateTime), CAST(N'2018-09-15T19:08:12.410' AS DateTime), 0, N'huy34234234@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1008, N'huyle@gmail.com', CAST(N'2018-09-15T19:11:10.240' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:11:10.240' AS DateTime), CAST(N'2018-09-15T19:11:10.240' AS DateTime), 0, N'huyle@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1009, N'huy123@gmail.com', CAST(N'2018-09-15T19:44:02.490' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:44:02.490' AS DateTime), CAST(N'2018-09-15T19:44:02.490' AS DateTime), 0, N'huy123@gmail.com')
GO
INSERT [dbo].[Profit] ([Id], [UserId], [DateReceive], [TotalAmountProfit], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1010, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T07:05:59.953' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:05:59.953' AS DateTime), CAST(N'2018-09-17T07:05:59.953' AS DateTime), 0, N'caubevodich9999@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Profit] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfitDaily] ON 
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14158, N'leduchuy1411@gmail.com', CAST(N'2018-09-12T19:54:43.563' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-12T19:54:43.563' AS DateTime), CAST(N'2018-09-13T03:32:04.640' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14159, N'leduchuy1411@gmail.com', CAST(N'2018-09-12T19:54:43.740' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-12T19:54:43.740' AS DateTime), CAST(N'2018-09-17T14:45:53.117' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14160, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T17:54:13.360' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-13T17:54:13.360' AS DateTime), CAST(N'2018-09-15T08:12:09.047' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14161, N'leduchuy1411@gmail.com', CAST(N'2018-09-13T17:54:13.487' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-13T17:54:13.487' AS DateTime), CAST(N'2018-09-15T08:12:02.420' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14162, N'leduchuy1411@gmail.com', CAST(N'2018-09-21T12:52:45.980' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:45.980' AS DateTime), CAST(N'2018-09-15T05:25:13.020' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14163, N'leduchuy1411@gmail.com', CAST(N'2018-09-20T12:52:45.993' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:45.993' AS DateTime), CAST(N'2018-09-15T08:24:18.837' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14164, N'leduchuy1411@gmail.com', CAST(N'2018-09-19T12:52:46.053' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.053' AS DateTime), CAST(N'2018-09-18T03:12:38.310' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14165, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T12:52:46.113' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.113' AS DateTime), CAST(N'2018-09-21T12:52:46.113' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14166, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T12:52:46.143' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.143' AS DateTime), CAST(N'2018-09-21T12:52:46.143' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14167, N'leduchuy1411@gmail.com', CAST(N'2018-09-16T12:52:46.183' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.183' AS DateTime), CAST(N'2018-09-21T12:52:46.183' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14168, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T12:52:46.227' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.227' AS DateTime), CAST(N'2018-09-21T12:52:46.227' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14169, N'leduchuy1411@gmail.com', CAST(N'2018-09-21T12:52:46.267' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.267' AS DateTime), CAST(N'2018-09-18T03:16:29.720' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14170, N'leduchuy1411@gmail.com', CAST(N'2018-09-20T12:52:46.293' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.293' AS DateTime), CAST(N'2018-09-18T03:16:20.213' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14171, N'leduchuy1411@gmail.com', CAST(N'2018-09-19T12:52:46.337' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.337' AS DateTime), CAST(N'2018-09-18T03:13:06.143' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14172, N'leduchuy1411@gmail.com', CAST(N'2018-09-18T12:52:46.367' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.367' AS DateTime), CAST(N'2018-09-21T12:52:46.367' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14173, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T12:52:46.400' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.400' AS DateTime), CAST(N'2018-09-21T12:52:46.400' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14174, N'leduchuy1411@gmail.com', CAST(N'2018-09-16T12:52:46.430' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.430' AS DateTime), CAST(N'2018-09-15T08:24:10.943' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14175, N'leduchuy1411@gmail.com', CAST(N'2018-09-15T12:52:46.470' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-21T12:52:46.470' AS DateTime), CAST(N'2018-09-21T12:52:46.470' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14176, N'leduchuy1411@gmail.com', CAST(N'2018-09-23T12:53:56.767' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-23T12:53:56.767' AS DateTime), CAST(N'2018-09-23T12:53:56.767' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14177, N'leduchuy1411@gmail.com', CAST(N'2018-09-22T12:53:56.793' AS DateTime), CAST(0.00975000 AS Decimal(18, 8)), CAST(N'2018-09-23T12:53:56.793' AS DateTime), CAST(N'2018-09-23T12:53:56.793' AS DateTime), 0, N'leduchuy1411@gmail.com', 1042, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14178, N'leduchuy1411@gmail.com', CAST(N'2018-09-23T12:53:56.830' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-23T12:53:56.830' AS DateTime), CAST(N'2018-09-23T12:53:56.830' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14179, N'leduchuy1411@gmail.com', CAST(N'2018-09-22T12:53:56.867' AS DateTime), CAST(0.00065000 AS Decimal(18, 8)), CAST(N'2018-09-23T12:53:56.867' AS DateTime), CAST(N'2018-09-15T07:33:46.453' AS DateTime), 0, N'leduchuy1411@gmail.com', 1043, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14180, N'abc23535@gmail.com', CAST(N'2018-09-15T15:37:02.517' AS DateTime), CAST(0.14950000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:37:02.517' AS DateTime), CAST(N'2018-09-15T15:37:02.517' AS DateTime), 0, N'abc23535@gmail.com', 1044, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14181, N'abc23535@gmail.com', CAST(N'2018-09-14T15:37:02.663' AS DateTime), CAST(0.14950000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:37:02.663' AS DateTime), CAST(N'2018-09-15T15:37:02.663' AS DateTime), 0, N'abc23535@gmail.com', 1044, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14182, N'abc23535@gmail.com', CAST(N'2018-09-13T15:37:02.710' AS DateTime), CAST(0.14950000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:37:02.710' AS DateTime), CAST(N'2018-09-15T15:37:02.710' AS DateTime), 0, N'abc23535@gmail.com', 1044, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14183, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T07:08:31.633' AS DateTime), CAST(0.00195000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:08:31.633' AS DateTime), CAST(N'2018-09-18T03:14:34.580' AS DateTime), 0, N'leduchuy1411@gmail.com', 1045, 1)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14184, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T07:08:31.657' AS DateTime), CAST(0.00390000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:08:31.660' AS DateTime), CAST(N'2018-09-17T07:08:31.660' AS DateTime), 0, N'leduchuy1411@gmail.com', 1046, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14185, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T07:08:31.660' AS DateTime), CAST(0.00130000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:08:31.660' AS DateTime), CAST(N'2018-09-17T07:08:31.660' AS DateTime), 0, N'leduchuy1411@gmail.com', 1047, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14186, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T08:27:32.730' AS DateTime), CAST(0.00195000 AS Decimal(18, 8)), CAST(N'2018-09-17T08:27:32.730' AS DateTime), CAST(N'2018-09-17T08:27:32.730' AS DateTime), 0, N'leduchuy1411@gmail.com', 1045, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14187, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T08:29:43.297' AS DateTime), CAST(0.00390000 AS Decimal(18, 8)), CAST(N'2018-09-17T08:29:43.297' AS DateTime), CAST(N'2018-09-17T08:29:43.297' AS DateTime), 0, N'leduchuy1411@gmail.com', 1046, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14188, N'leduchuy1411@gmail.com', CAST(N'2018-09-17T08:35:54.737' AS DateTime), CAST(0.00130000 AS Decimal(18, 8)), CAST(N'2018-09-17T08:35:54.737' AS DateTime), CAST(N'2018-09-17T08:35:54.737' AS DateTime), 0, N'leduchuy1411@gmail.com', 1047, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14189, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T19:22:51.813' AS DateTime), CAST(0.00300000 AS Decimal(18, 8)), CAST(N'2018-09-17T19:22:51.817' AS DateTime), CAST(N'2018-09-17T19:22:51.817' AS DateTime), 0, N'caubevodich9999@gmail.com', 1048, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14190, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T19:22:51.863' AS DateTime), CAST(0.00300000 AS Decimal(18, 8)), CAST(N'2018-09-17T19:22:51.863' AS DateTime), CAST(N'2018-09-17T19:22:51.863' AS DateTime), 0, N'caubevodich9999@gmail.com', 1049, 0)
GO
INSERT [dbo].[ProfitDaily] ([Id], [UserId], [DateReceive], [AmountDaily], [CreatedAt], [ModifiedAt], [IsDeleted], [Name], [DepositId], [Status]) VALUES (14191, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T19:22:51.867' AS DateTime), CAST(0.00300000 AS Decimal(18, 8)), CAST(N'2018-09-17T19:22:51.867' AS DateTime), CAST(N'2018-09-17T19:22:51.867' AS DateTime), 0, N'caubevodich9999@gmail.com', 1050, 0)
GO
SET IDENTITY_INSERT [dbo].[ProfitDaily] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiveNetworkComission] ON 
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (1, N'leduchuy1411@gmail.com', CAST(N'2018-09-14T13:06:01.273' AS DateTime), CAST(2.00000000 AS Decimal(18, 8)), CAST(N'2018-08-11T14:19:37.980' AS DateTime), CAST(N'2018-09-12T14:19:37.980' AS DateTime), 0, N'leduchuy1411@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (2, N'huy567810@gmail.com', CAST(N'2018-09-12T16:01:44.107' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-12T16:01:44.107' AS DateTime), CAST(N'2018-09-12T16:01:44.107' AS DateTime), 0, N'huy567810@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (3, N'bao23@gmail.com', CAST(N'2018-09-14T06:50:35.413' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-14T06:50:35.413' AS DateTime), CAST(N'2018-09-14T06:50:35.413' AS DateTime), 0, N'bao23@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (4, N'bao123@gmail.com', CAST(N'2018-09-15T15:31:14.627' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:31:14.627' AS DateTime), CAST(N'2018-09-15T15:31:14.627' AS DateTime), 0, N'bao123@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (5, N'abc23535@gmail.com', CAST(N'2018-09-15T15:37:02.350' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T15:37:02.350' AS DateTime), CAST(N'2018-09-15T15:37:02.350' AS DateTime), 0, N'abc23535@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (6, N'huy343@gmail.com', CAST(N'2018-09-15T19:07:06.557' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:07:06.557' AS DateTime), CAST(N'2018-09-15T19:07:06.557' AS DateTime), 0, N'huy343@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (7, N'huy34234234@gmail.com', CAST(N'2018-09-15T19:08:12.447' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:08:12.447' AS DateTime), CAST(N'2018-09-15T19:08:12.447' AS DateTime), 0, N'huy34234234@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (8, N'huyle@gmail.com', CAST(N'2018-09-15T19:11:10.247' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:11:10.247' AS DateTime), CAST(N'2018-09-15T19:11:10.247' AS DateTime), 0, N'huyle@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (9, N'huy123@gmail.com', CAST(N'2018-09-15T19:44:03.157' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-15T19:44:03.157' AS DateTime), CAST(N'2018-09-15T19:44:03.157' AS DateTime), 0, N'huy123@gmail.com')
GO
INSERT [dbo].[ReceiveNetworkComission] ([Id], [UserId], [DateReceiveComission], [AmountRemain], [CreatedAt], [ModifiedAt], [IsDeleted], [Name]) VALUES (10, N'caubevodich9999@gmail.com', CAST(N'2018-09-17T07:06:00.313' AS DateTime), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-17T07:06:00.313' AS DateTime), CAST(N'2018-09-17T07:06:00.313' AS DateTime), 0, N'caubevodich9999@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[ReceiveNetworkComission] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (2, NULL, N'leduchuy1411@gmail.com', N'QQMsRcosFHWSTw3iihJkvkzAVVwDh6pjmf', N'QW9GJJc3zL41QegPNXzkBv38JgtmG5MSDq', N'8461d20e1d61682b96f187e7e7619959b95c2be6c4b9dce7e2ebb1471fc1ba19', CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-07-22T13:24:31.050' AS DateTime), CAST(N'2018-07-22T13:24:31.050' AS DateTime), 77258, 1, N'LTCTEST', CAST(0.00041200 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Confirmed', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (3, NULL, N'leduchuy1411@gmail.com', N'QQMsRcosFHWSTw3iihJkvkzAVVwDh6pjmf', N'QW9GJJc3zL41QegPNXzkBv38JgtmG5MSDq', N'85e3944c81588edbd5f8259a33e31b892a6df584d32f43e02bee08e63ea91275', CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-01T05:02:56.707' AS DateTime), CAST(N'2018-09-01T05:02:56.707' AS DateTime), 34078, 1, N'LTCTEST', CAST(0.00133000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Confirmed', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (6, NULL, N'leduchuy1411@gmail.com', N'QQMsRcosFHWSTw3iihJkvkzAVVwDh6pjmf', N'QiJyabNvdNd74SaLkhcLZm7YBzR51PEg7i', NULL, CAST(0.09500000 AS Decimal(18, 8)), CAST(N'2018-09-01T06:36:36.047' AS DateTime), CAST(N'2018-09-01T06:36:36.047' AS DateTime), 0, 0, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00500000 AS Decimal(18, 8)), N'Pending', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (7, NULL, N'leduchuy1411@gmail.com', N'QiRvYqJGidYXn9ftCAfg5RBWaAApJWpYaD', N'QiRvYqJGidYXn9ftCAfg5RBWaAApJWpYaD', NULL, CAST(1.90000000 AS Decimal(18, 8)), CAST(N'2018-09-15T18:43:56.937' AS DateTime), CAST(N'2018-09-15T18:43:56.937' AS DateTime), 0, 1, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.10000000 AS Decimal(18, 8)), N'Pending', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (10, N'leduchuy1411@gmail.com', N'leduchuy1411@gmail.com', N'', N'', N'', CAST(0.10000000 AS Decimal(18, 8)), CAST(N'2018-09-18T03:27:42.770' AS DateTime), CAST(N'2018-09-18T03:27:42.770' AS DateTime), 0, 0, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Pending', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (11, N'leduchuy1411@gmail.com', N'leduchuy1411@gmail.com', N'', N'', N'e0e9f44d43e0725b04d4ceb42697ade4ba0668085cfe36191f32f2a9e6d07d60', CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-18T07:26:48.290' AS DateTime), CAST(N'2018-09-18T07:26:48.780' AS DateTime), 755903, 1, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Confirmed', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (12, N'leduchuy1411@gmail.com', N'leduchuy1411@gmail.com', N'', N'', N'b841c670f6a21070fbc9e2c8a37dc075cba93e69fb8a761d1c341a9708c1ccf2', CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-18T07:26:57.010' AS DateTime), CAST(N'2018-09-18T07:26:57.010' AS DateTime), 755903, 1, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Confirmed', 0)
GO
INSERT [dbo].[Transaction] ([Id], [Name], [UserId], [From], [To], [TxId], [Amount], [CreatedAt], [ModifiedAt], [TotalConfirm], [Type], [Network], [NetworkFee], [ExchangeFee], [Status], [IsDeleted]) VALUES (13, N'leduchuy1411@gmail.com', N'leduchuy1411@gmail.com', N'', N'', N'9ceefe572f3bcc2afbfa0b6738fec18ea5296b8024deb600ea12a88907231926', CAST(0.05000000 AS Decimal(18, 8)), CAST(N'2018-09-18T07:41:43.637' AS DateTime), CAST(N'2018-09-18T07:41:43.637' AS DateTime), 4, 1, N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'Confirmed', 0)
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
SET IDENTITY_INSERT [dbo].[Wallet] ON 
GO
INSERT [dbo].[Wallet] ([Id], [UserId], [Name], [Address], [Symbol], [Network], [AvailableBalance], [PendingReceivedBalance], [CreatedAt], [ModifiedAt], [IsDefault], [IsDeleted]) VALUES (1, N'tuanitpro@gmail.com', N'tuanitpro@gmail.com_21072018020750SA', N'QQMsRcosFHWSTw3iihJkvkzAVVwDh6pjmf', N'LTC', N'LTCTEST', CAST(0.09417600 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-07-22T15:37:23.327' AS DateTime), CAST(N'2018-09-01T06:40:43.010' AS DateTime), 1, 0)
GO
INSERT [dbo].[Wallet] ([Id], [UserId], [Name], [Address], [Symbol], [Network], [AvailableBalance], [PendingReceivedBalance], [CreatedAt], [ModifiedAt], [IsDefault], [IsDeleted]) VALUES (2, N'test1@gmail.com', N'test1@gmail.com', N'QiJyabNvdNd74SaLkhcLZm7YBzR51PEg7i', N'LTC', N'LTCTEST', CAST(0.14500000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-01T05:09:59.317' AS DateTime), CAST(N'2018-09-01T08:40:52.120' AS DateTime), 1, 0)
GO
INSERT [dbo].[Wallet] ([Id], [UserId], [Name], [Address], [Symbol], [Network], [AvailableBalance], [PendingReceivedBalance], [CreatedAt], [ModifiedAt], [IsDefault], [IsDeleted]) VALUES (4, N'abc23535@gmail.com', N'abc23535@gmail.com', N'QUcJ9L6xdV5qmL4ZNfaFphH66BTxr9URYS', N'LTC', N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-11T18:43:40.623' AS DateTime), CAST(N'2018-09-11T18:43:40.623' AS DateTime), 1, 0)
GO
INSERT [dbo].[Wallet] ([Id], [UserId], [Name], [Address], [Symbol], [Network], [AvailableBalance], [PendingReceivedBalance], [CreatedAt], [ModifiedAt], [IsDefault], [IsDeleted]) VALUES (5, N'caubevodich9999@gmail.com', N'caubevodich9999@gmail.com', N'QbBNvRfbgPzLK4t1BbgpE7prVCcZdiTKcA', N'LTC', N'LTCTEST', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-16T14:41:26.367' AS DateTime), CAST(N'2018-09-16T14:41:26.367' AS DateTime), 1, 0)
GO
INSERT [dbo].[Wallet] ([Id], [UserId], [Name], [Address], [Symbol], [Network], [AvailableBalance], [PendingReceivedBalance], [CreatedAt], [ModifiedAt], [IsDefault], [IsDeleted]) VALUES (6, N'leduchuy1411@gmail.com', N'leduchuy1411@gmail.com', N'3BxK73y9D3VTL8fSJAmMbom8zSrL3teA2f', N'LTC', N'LTC', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), CAST(N'2018-09-18T08:04:24.673' AS DateTime), CAST(N'2018-09-18T08:04:24.673' AS DateTime), 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Wallet] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 18/09/2018 3:13:20 CH ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[Mega] ADD  CONSTRAINT [DF_Mega_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Mega] ADD  CONSTRAINT [DF_Mega_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[Mega] ADD  CONSTRAINT [DF_Mega_IsClosed]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MegaPlay] ADD  CONSTRAINT [DF_MegaPlay_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[MegaPlay] ADD  CONSTRAINT [DF_MegaPlay_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[MegaRoom] ADD  CONSTRAINT [DF_Table1_CreateTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[MegaRoom] ADD  CONSTRAINT [DF_MegaRoom_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedTime]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_CusINCOME_CreateTime]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_CusINCOME_ModifiedTime]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_CusINCOME_ConfirmsNo]  DEFAULT ((0)) FOR [TotalConfirm]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_CusTransaction_IsINCOME]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Wallet] ADD  CONSTRAINT [DF_Wallet_Symbol]  DEFAULT (N'LTC') FOR [Symbol]
GO
ALTER TABLE [dbo].[Wallet] ADD  CONSTRAINT [DF_Wallet_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Mega]  WITH CHECK ADD  CONSTRAINT [FK_Mega_MegaRoom] FOREIGN KEY([MegaRoomId])
REFERENCES [dbo].[MegaRoom] ([Id])
GO
ALTER TABLE [dbo].[Mega] CHECK CONSTRAINT [FK_Mega_MegaRoom]
GO
ALTER TABLE [dbo].[MegaPlay]  WITH CHECK ADD  CONSTRAINT [FK_MegaPlay_Customer] FOREIGN KEY([CustId])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[MegaPlay] CHECK CONSTRAINT [FK_MegaPlay_Customer]
GO
ALTER TABLE [dbo].[MegaPlay]  WITH CHECK ADD  CONSTRAINT [FK_MegaPlay_MegaRoom] FOREIGN KEY([MegaRoomId])
REFERENCES [dbo].[MegaRoom] ([Id])
GO
ALTER TABLE [dbo].[MegaPlay] CHECK CONSTRAINT [FK_MegaPlay_MegaRoom]
GO
USE [master]
GO
ALTER DATABASE [CryptoIO] SET  READ_WRITE 
GO
