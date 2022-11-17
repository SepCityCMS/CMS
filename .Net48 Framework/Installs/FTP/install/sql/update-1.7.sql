ALTER TABLE Uploads ADD ControlID [varchar] (200)
GO

ALTER TABLE Uploads ADD UserRates [int]
ALTER TABLE Uploads ADD TotalRates [int]
GO

UPDATE Uploads SET UserRates='0', TotalRates='0'
GO

ALTER TABLE ShopProducts ADD [SalesStartDate] [datetime]
ALTER TABLE ShopProducts ADD [SalesEndDate] [datetime]
GO

ALTER TABLE RStateProperty ADD [NumBedrooms2] [decimal]
GO
ALTER TABLE RStateProperty ADD [NumBathrooms2] [decimal]
GO

UPDATE RStateProperty SET NumBedrooms2=NumBedrooms
GO
UPDATE RStateProperty SET NumBathrooms2=NumBathrooms
GO

ALTER TABLE RStateProperty DROP COLUMN [NumBedrooms]
GO
ALTER TABLE RStateProperty DROP COLUMN [NumBathrooms]
GO

ALTER TABLE RStateProperty ADD [NumBedrooms] [decimal]
GO
ALTER TABLE RStateProperty ADD [NumBathrooms] [decimal]
GO

UPDATE RStateProperty SET NumBedrooms=NumBedrooms2
GO
UPDATE RStateProperty SET NumBathrooms=NumBathrooms2
GO

ALTER TABLE RStateProperty DROP COLUMN [NumBedrooms2]
GO
ALTER TABLE RStateProperty DROP COLUMN [NumBathrooms2]
GO

UPDATE ModulesNPages SET UserPageName='stocks.aspx', PageiD='15', MenuID='3' WHERE ModuleID='15'
GO
UPDATE ModulesNPages SET UserPageName='speakers.aspx' WHERE ModuleID='50'
GO
UPDATE PortalPages SET UserPageName='speakers.aspx' WHERE PageID='50'
GO

ALTER TABLE Members DROP COLUMN TwilioVideoURL
GO

ALTER TABLE AuctionFeedback ADD AdID [float] not null
GO

DELETE FROM AuctionFeedback
GO

ALTER TABLE AuctionFeedback DROP CONSTRAINT pk_ID_AuctionFeedback
GO

DROP INDEX AuctionFeedback.ItemID
GO

ALTER TABLE AuctionFeedback DROP COLUMN ItemID
GO

ALTER TABLE AuctionFeedback ADD FeedbackID [float] not null DEFAULT('1')
GO

ALTER TABLE [AuctionFeedback] WITH NOCHECK ADD CONSTRAINT [pk_FeedbackID_AuctionFeedback] PRIMARY KEY CLUSTERED ([FeedbackID]) ON [PRIMARY] 
GO

ALTER TABLE AuctionFeedback DROP COLUMN ID
GO
