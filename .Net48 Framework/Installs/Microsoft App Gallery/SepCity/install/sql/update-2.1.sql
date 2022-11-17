ALTER TABLE Categories ADD [FeedID] [float]
GO
ALTER TABLE ShopProducts ADD [FeedID] [float]
GO
ALTER TABLE Wholesale2b_Feeds ADD AccessKeys varchar(4000)
GO
ALTER TABLE Wholesale2b_Feeds ADD AccessHide bit
GO
ALTER TABLE Wholesale2b_Feeds ADD ExcPortalSecurity bit
GO
ALTER TABLE Wholesale2b_Feeds ADD Sharing bit
GO
ALTER TABLE Wholesale2b_Feeds ADD PortalIDs varchar(max)
GO
DELETE FROM ShopProducts WHERE ImportID IS NOT NULL OR ImportID <> ''
GO
ALTER TABLE Categories ADD [FileName] [varchar](25)
GO
ALTER TABLE ShopProducts ADD [ExcludeDiscount] [bit]
GO
CREATE INDEX [ExcludeDiscount] ON [ShopProducts]([ExcludeDiscount])
GO
CREATE INDEX [FeedID] ON [Categories]([FeedID])
GO
CREATE INDEX [FeedID] ON [ShopProducts]([FeedID])
GO