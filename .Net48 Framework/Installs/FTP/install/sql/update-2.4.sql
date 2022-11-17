ALTER TABLE ShopStores DROP COLUMN StartDate
GO

ALTER TABLE ShopStores ADD DateAdded [datetime]
GO

ALTER TABLE ShopProducts ADD [StoreID] [float] NULL
GO

ALTER TABLE ShopStores ADD [UserID] [varchar] (40) NULL
GO

ALTER TABLE ShopProducts ADD [UserID] [varchar] (40) NULL
GO

CREATE INDEX [StoreID] ON [ShopProducts]([StoreID])
GO

CREATE INDEX [UserID] ON [ShopStores]([UserID])
GO

CREATE INDEX [UserID] ON [ShopProducts]([UserID])
GO

CREATE INDEX [PortalID] ON [ShopStores]([PortalID])
GO

ALTER TABLE ShopShippingMethods DROP COLUMN [Calculation]
GO

ALTER TABLE ShopShippingMethods ADD [StoreID] [float] NULL
GO
CREATE INDEX [StoreID] ON [ShopShippingMethods]([StoreID])
GO

CREATE TABLE [dbo].[PNQVotes](
	[PollID] [Float] NOT NULL,
	[OptionID] [Float] NOT NULL,
	[PortalID] [Float] NOT NULL,
	[NumVotes] [int] NOT NULL,
 CONSTRAINT [pk_PollID_PNQVotes] PRIMARY KEY CLUSTERED 
(
	[PollID] ASC,[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE INDEX [PortalID] ON [PNQVotes]([PortalID])
GO

ALTER TABLE PNQOptions DROP COLUMN SelectedCount
GO

ALTER TABLE dbo.PNQQuestions ADD [UserID] [VARCHAR] (40)
GO

ALTER TABLE PNQQuestions ADD [VoteKeys] [NTEXT]
GO

UPDATE PNQQuestions SET VoteKeys='|1|,|2|,|3|,|4|'
GO

UPDATE PNQQuestions SET UserID=(SELECT TOP 1 AccessKeys FROM Members WHERE AccessClass='2')
GO

ALTER TABLE Invoices_Products ADD [StoreID] [FLOAT] NULL
GO

CREATE INDEX [StoreID] ON [Invoices_Products]([StoreID])
GO

ALTER TABLE Invoices_Products ADD [ShippingMethodID] [FLOAT] NULL
GO

CREATE INDEX [ShippingMethodID] ON [Invoices_Products]([ShippingMethodID])
GO

