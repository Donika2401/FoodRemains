USE [master]
GO
/****** Object:  Database [FoodRemains]    Script Date: 2022-04-17 6:58:11 PM ******/
CREATE DATABASE [FoodRemains]

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodRemains].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodRemains] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FoodRemains] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FoodRemains] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FoodRemains] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FoodRemains] SET ARITHABORT OFF 
GO
ALTER DATABASE [FoodRemains] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FoodRemains] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FoodRemains] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FoodRemains] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FoodRemains] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FoodRemains] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FoodRemains] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FoodRemains] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FoodRemains] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FoodRemains] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FoodRemains] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FoodRemains] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FoodRemains] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FoodRemains] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FoodRemains] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FoodRemains] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FoodRemains] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FoodRemains] SET RECOVERY FULL 
GO
ALTER DATABASE [FoodRemains] SET  MULTI_USER 
GO
ALTER DATABASE [FoodRemains] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FoodRemains] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FoodRemains] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FoodRemains] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FoodRemains] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FoodRemains] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FoodRemains', N'ON'
GO
ALTER DATABASE [FoodRemains] SET QUERY_STORE = OFF
GO
USE [FoodRemains]
GO
/****** Object:  Table [dbo].[FAQMaster]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQMaster](
	[FAQID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](500) NULL,
	[Answer] [varchar](max) NULL,
 CONSTRAINT [PK_FAQMaster] PRIMARY KEY CLUSTERED 
(
	[FAQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackMaster]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackMaster](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackType] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[EmailID] [varchar](150) NULL,
	[RestaurantID] [int] NULL,
 CONSTRAINT [PK_FeedbackMaster] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Name] [varchar](50) NULL,
	[Latitude] [numeric](18, 6) NULL,
	[Longitude] [numeric](18, 6) NULL,
	[Description] [varchar](300) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantID] [int] NULL,
	[PostedFoodID] [int] NULL,
	[Quantity] [int] NULL,
	[CustomerFirstName] [varchar](50) NULL,
	[CustomerLastName] [varchar](50) NULL,
	[EmailID] [varchar](150) NULL,
	[ContactNo] [varchar](15) NULL,
	[OrderReferenceNo] [varchar](8) NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OwnerMaster]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OwnerMaster](
	[OwnerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[EmailID] [varchar](150) NULL,
	[ContactNo] [varchar](15) NULL,
	[Password] [varchar](20) NULL,
	[BusinessName] [varchar](50) NULL,
	[Website] [varchar](100) NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_OwnerMaster] PRIMARY KEY CLUSTERED 
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostFoodMaster]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostFoodMaster](
	[PostedFoodID] [int] IDENTITY(1,1) NOT NULL,
	[FoodItem] [varchar](200) NULL,
	[Quantity] [int] NULL,
	[ServesPerson] [int] NULL,
	[Description] [varchar](max) NULL,
	[RestaurantID] [int] NULL,
 CONSTRAINT [PK_PostFoodMaster] PRIMARY KEY CLUSTERED 
(
	[PostedFoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantMaster]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantMaster](
	[RestaurantID] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantName] [varchar](50) NULL,
	[Location] [varchar](200) NULL,
	[OpeningHour] [time](7) NULL,
	[ClosingHour] [time](7) NULL,
	[AvailableService] [varchar](150) NULL,
	[Cuisine] [varchar](50) NULL,
	[Photo] [varchar](max) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_RestaurantMaster] PRIMARY KEY CLUSTERED 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogDetails]    Script Date: 2022-04-17 6:58:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogDetails](
	[UserLogID] [int] IDENTITY(1,1) NOT NULL,
	[OwnerID] [int] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
 CONSTRAINT [PK_UserLogDetails] PRIMARY KEY CLUSTERED 
(
	[UserLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FeedbackMaster]  WITH CHECK ADD  CONSTRAINT [FK_FeedbackMaster_RestaurantMaster] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[RestaurantMaster] ([RestaurantID])
GO
ALTER TABLE [dbo].[FeedbackMaster] CHECK CONSTRAINT [FK_FeedbackMaster_RestaurantMaster]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_PostFoodMaster] FOREIGN KEY([PostedFoodID])
REFERENCES [dbo].[PostFoodMaster] ([PostedFoodID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_PostFoodMaster]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_RestaurantMaster] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[RestaurantMaster] ([RestaurantID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_RestaurantMaster]
GO
ALTER TABLE [dbo].[PostFoodMaster]  WITH CHECK ADD  CONSTRAINT [FK_PostFoodMaster_RestaurantMaster] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[RestaurantMaster] ([RestaurantID])
GO
ALTER TABLE [dbo].[PostFoodMaster] CHECK CONSTRAINT [FK_PostFoodMaster_RestaurantMaster]
GO
ALTER TABLE [dbo].[RestaurantMaster]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantMaster_OwnerMaster] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[OwnerMaster] ([OwnerID])
GO
ALTER TABLE [dbo].[RestaurantMaster] CHECK CONSTRAINT [FK_RestaurantMaster_OwnerMaster]
GO
ALTER TABLE [dbo].[UserLogDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserLogDetails_OwnerMaster] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[OwnerMaster] ([OwnerID])
GO
ALTER TABLE [dbo].[UserLogDetails] CHECK CONSTRAINT [FK_UserLogDetails_OwnerMaster]
GO
USE [master]
GO
ALTER DATABASE [FoodRemains] SET  READ_WRITE 
GO
