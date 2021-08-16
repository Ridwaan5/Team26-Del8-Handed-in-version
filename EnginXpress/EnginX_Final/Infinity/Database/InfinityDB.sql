USE [master]
GO
/****** Object:  Database [Infinity_Db]    Script Date: 2021/08/02 19:39:02 ******/
CREATE DATABASE [Infinity_Db]
GO
ALTER DATABASE [Infinity_Db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Infinity_Db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Infinity_Db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Infinity_Db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Infinity_Db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Infinity_Db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Infinity_Db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Infinity_Db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Infinity_Db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Infinity_Db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Infinity_Db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Infinity_Db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Infinity_Db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Infinity_Db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Infinity_Db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Infinity_Db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Infinity_Db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Infinity_Db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Infinity_Db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Infinity_Db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Infinity_Db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Infinity_Db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Infinity_Db] SET RECOVERY FULL 
GO
ALTER DATABASE [Infinity_Db] SET  MULTI_USER 
GO
ALTER DATABASE [Infinity_Db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Infinity_Db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Infinity_Db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Infinity_Db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Infinity_Db] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Infinity_Db', N'ON'
GO
ALTER DATABASE [Infinity_Db] SET QUERY_STORE = OFF
GO
USE [Infinity_Db]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[CityId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressLine1] [varchar](50) NOT NULL,
	[AddressLine2] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[adminID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Surname] [varchar](max) NOT NULL,
	[Contact number] [varchar](max) NOT NULL,
	[email address] [varchar](max) NOT NULL,
	[Address] [varchar](max) NOT NULL,
 CONSTRAINT [PK_administrator] PRIMARY KEY CLUSTERED 
(
	[adminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cartID] [int] NOT NULL,
	[ProductName] [varchar](max) NOT NULL,
	[ProductDescription] [varchar](max) NOT NULL,
	[ProductPrice] [decimal](18, 0) NOT NULL,
	[DeliveryAmount] [decimal](18, 0) NOT NULL,
	[CaerTotal] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[cartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[cityID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Code] [varchar](max) NOT NULL,
 CONSTRAINT [PK_city] PRIMARY KEY CLUSTERED 
(
	[cityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Information]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Information](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[CompanyPhysicalAddress] [varchar](max) NULL,
	[CompanyTelephoneNumber] [varchar](50) NULL,
	[CompanyEmailAddress] [varchar](50) NULL,
	[CompanyLogo] [varchar](50) NULL,
 CONSTRAINT [PK_Company_Information] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customerID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Surname] [varchar](max) NOT NULL,
	[Contact number] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Customer location] [varchar](max) NOT NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[customerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer_Order]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_Order](
	[orderID] [int] NOT NULL,
	[customerID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[deliveryID] [int] NOT NULL,
	[Employee Name] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [varchar](max) NOT NULL,
 CONSTRAINT [PK_delivery] PRIMARY KEY CLUSTERED 
(
	[deliveryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery order status]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery order status](
	[deliveryorderstatusID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_delivery order status] PRIMARY KEY CLUSTERED 
(
	[deliveryorderstatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[employeeID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Surname] [varchar](max) NOT NULL,
	[mobile number] [varchar](max) NOT NULL,
	[Email address] [varchar](max) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[Date of birth] [datetime] NOT NULL,
	[Username] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[User role] [int] NOT NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[employeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_feedback] PRIMARY KEY CLUSTERED 
(
	[feedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[inventoryID] [int] NOT NULL,
	[Inventory Description] [varchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[inventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log-In]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log-In](
	[loginID] [int] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_log-in] PRIMARY KEY CLUSTERED 
(
	[loginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log-out]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log-out](
	[logoutID] [int] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_log-out] PRIMARY KEY CLUSTERED 
(
	[logoutID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderID] [int] NOT NULL,
	[Order Description] [varchar](max) NOT NULL,
	[Order Number] [varchar](max) NOT NULL,
	[Product Name] [varchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Order Status] [varchar](max) NOT NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Product]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Product](
	[productID] [int] NOT NULL,
	[orderID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Status]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Status](
	[orderstatusID] [int] NOT NULL,
	[Order_Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_order_status] PRIMARY KEY CLUSTERED 
(
	[orderstatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[paymentID] [int] NOT NULL,
	[Payment_Method] [varchar](max) NOT NULL,
	[Payment_Amount] [varchar](max) NOT NULL,
	[Payment_Date] [datetime] NOT NULL,
	[Payment_Time] [time](7) NOT NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[paymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[PriceId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productID] [int] NOT NULL,
	[ProductName] [varchar](max) NOT NULL,
	[ProductSize] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product Category]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product Category](
	[productcategoryID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_product category] PRIMARY KEY CLUSTERED 
(
	[productcategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product Type]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product Type](
	[producttypeID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_product type] PRIMARY KEY CLUSTERED 
(
	[producttypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Content]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Content](
	[ProductContentId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Product_Content] PRIMARY KEY CLUSTERED 
(
	[ProductContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[provinceID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
 CONSTRAINT [PK_province] PRIMARY KEY CLUSTERED 
(
	[provinceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[SizeId] [int] NOT NULL,
	[SizeDescription] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[SizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[stockID] [int] NOT NULL,
	[Stock_code] [varchar](max) NOT NULL,
	[Stock_Amount] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Stock_Item] [varchar](max) NOT NULL,
 CONSTRAINT [PK_stock] PRIMARY KEY CLUSTERED 
(
	[stockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock type]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock type](
	[stocktypeID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_stock type] PRIMARY KEY CLUSTERED 
(
	[stocktypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock_Inventory]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock_Inventory](
	[inventoryID] [int] NOT NULL,
	[stockID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock_Line]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock_Line](
	[StockLineId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Reason] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Stock_Line] PRIMARY KEY CLUSTERED 
(
	[StockLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockTake]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTake](
	[StockTakeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_StockTake] PRIMARY KEY CLUSTERED 
(
	[StockTakeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[supplierID] [int] NOT NULL,
	[Company Name] [varchar](max) NOT NULL,
	[Contact Name] [varchar](max) NOT NULL,
	[Contact Number] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[Website] [varchar](max) NOT NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[supplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier Order Status]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier Order Status](
	[SupplierOrderStatusDescription] [varchar](max) NOT NULL,
	[sorderID] [int] NOT NULL,
 CONSTRAINT [PK_supplier order status] PRIMARY KEY CLUSTERED 
(
	[sorderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier_Order]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_Order](
	[supplierorderID] [int] NOT NULL,
	[SupplierOrderNumber] [varchar](max) NOT NULL,
	[SupplierOrderDate] [datetime] NOT NULL,
	[SupplierOrderDescription] [varchar](max) NOT NULL,
	[SupplierOrderQuantity] [int] NOT NULL,
	[SupplierOrderNote] [varchar](max) NOT NULL,
 CONSTRAINT [PK_supplier_order] PRIMARY KEY CLUSTERED 
(
	[supplierorderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier_Order_Line]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_Order_Line](
	[supplierorderID] [int] NOT NULL,
	[stockID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier_Type]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_Type](
	[suppliertypeID] [int] NOT NULL,
	[Supplier Type Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_supplier_type] PRIMARY KEY CLUSTERED 
(
	[suppliertypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] NOT NULL,
	[Username] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User Role]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User Role](
	[userroleID] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_user role] PRIMARY KEY CLUSTERED 
(
	[userroleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Description]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Description](
	[User_StatusId] [int] IDENTITY(1,1) NOT NULL,
	[UserStatusDescription] [varchar](50) NULL,
 CONSTRAINT [PK_User_Description] PRIMARY KEY CLUSTERED 
(
	[User_StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vat]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vat](
	[VatId] [int] IDENTITY(1,1) NOT NULL,
	[VatAmount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Vat] PRIMARY KEY CLUSTERED 
(
	[VatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WriteOffs]    Script Date: 2021/08/02 19:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WriteOffs](
	[WriteOffId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Reason] [varchar](50) NOT NULL,
 CONSTRAINT [PK_WriteOffs] PRIMARY KEY CLUSTERED 
(
	[WriteOffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_131]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_131] ON [dbo].[Customer_Order]
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_134]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_134] ON [dbo].[Customer_Order]
(
	[customerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_249]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_249] ON [dbo].[Order_Product]
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_252]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_252] ON [dbo].[Order_Product]
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_277]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_277] ON [dbo].[Stock_Inventory]
(
	[inventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_280]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_280] ON [dbo].[Stock_Inventory]
(
	[stockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_238]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_238] ON [dbo].[Supplier_Order_Line]
(
	[supplierorderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [fkIdx_260]    Script Date: 2021/08/02 19:39:02 ******/
CREATE NONCLUSTERED INDEX [fkIdx_260] ON [dbo].[Supplier_Order_Line]
(
	[stockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer_Order]  WITH CHECK ADD  CONSTRAINT [FK_130] FOREIGN KEY([orderID])
REFERENCES [dbo].[Order] ([orderID])
GO
ALTER TABLE [dbo].[Customer_Order] CHECK CONSTRAINT [FK_130]
GO
ALTER TABLE [dbo].[Customer_Order]  WITH CHECK ADD  CONSTRAINT [FK_133] FOREIGN KEY([customerID])
REFERENCES [dbo].[Customer] ([customerID])
GO
ALTER TABLE [dbo].[Customer_Order] CHECK CONSTRAINT [FK_133]
GO
ALTER TABLE [dbo].[Order_Product]  WITH CHECK ADD  CONSTRAINT [FK_248] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([productID])
GO
ALTER TABLE [dbo].[Order_Product] CHECK CONSTRAINT [FK_248]
GO
ALTER TABLE [dbo].[Order_Product]  WITH CHECK ADD  CONSTRAINT [FK_251] FOREIGN KEY([orderID])
REFERENCES [dbo].[Order] ([orderID])
GO
ALTER TABLE [dbo].[Order_Product] CHECK CONSTRAINT [FK_251]
GO
ALTER TABLE [dbo].[Stock_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_276] FOREIGN KEY([inventoryID])
REFERENCES [dbo].[Inventory] ([inventoryID])
GO
ALTER TABLE [dbo].[Stock_Inventory] CHECK CONSTRAINT [FK_276]
GO
ALTER TABLE [dbo].[Stock_Inventory]  WITH CHECK ADD  CONSTRAINT [FK_279] FOREIGN KEY([stockID])
REFERENCES [dbo].[Stock] ([stockID])
GO
ALTER TABLE [dbo].[Stock_Inventory] CHECK CONSTRAINT [FK_279]
GO
ALTER TABLE [dbo].[Supplier_Order_Line]  WITH CHECK ADD  CONSTRAINT [FK_237] FOREIGN KEY([supplierorderID])
REFERENCES [dbo].[Supplier_Order] ([supplierorderID])
GO
ALTER TABLE [dbo].[Supplier_Order_Line] CHECK CONSTRAINT [FK_237]
GO
ALTER TABLE [dbo].[Supplier_Order_Line]  WITH CHECK ADD  CONSTRAINT [FK_259] FOREIGN KEY([stockID])
REFERENCES [dbo].[Stock] ([stockID])
GO
ALTER TABLE [dbo].[Supplier_Order_Line] CHECK CONSTRAINT [FK_259]
GO
USE [master]
GO
ALTER DATABASE [Infinity_Db] SET  READ_WRITE 
GO
