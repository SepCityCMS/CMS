update uploads set controlid='Images' where moduleid='41'
GO

update uploads set controlid='Pictures' where moduleid='63'
GO

CREATE TABLE [dbo].[ShopStores](
	[StoreID] [Float] NOT NULL,
	[StoreName] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[CompanyName] [nvarchar](100) NULL,
	[StreetAddress] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Country] [varchar](2) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[FaxNumber] [nvarchar](30) NULL,
	[WebsiteURL] [varchar](250) NULL,
	[StartDate] [int] NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_StoreID_ShopStores] PRIMARY KEY CLUSTERED 
(
	[StoreID] ASC,[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ShopShippingMethods](
	[MethodID] [Float] NOT NULL,
	[MethodName] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[Calculation] [int] NULL,
	[Carrier] [nvarchar](50) NULL,
	[ShippingService] [nvarchar](100) NULL,
	[DeliveryTime] [nvarchar](100) NULL,
	[WeightLimit] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[DateDeleted] [datetime] NULL,
	[PortalID] [float] NULL,
 CONSTRAINT [pk_MethodID_ShopShippingMethods] PRIMARY KEY CLUSTERED 
(
	[MethodID] ASC,[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE ShopProducts ADD [MinQuantity] [int] NULL
GO

ALTER TABLE ShopProducts ADD [MaxQuantity] [int] NULL
GO

DROP TABLE PageStats
GO

delete from modulesnpages where moduleid='26'
GO
