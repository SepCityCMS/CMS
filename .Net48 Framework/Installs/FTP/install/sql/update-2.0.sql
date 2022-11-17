CREATE INDEX [LookupCat] ON [CategoriesPortals]([CatID],[PortalID])
GO
CREATE INDEX [LookupCat] ON [CategoriesPages]([CatID],[ModuleID],[PortalID])
GO
CREATE INDEX [LookupCat] ON [CategoriesModules]([CatID],[ModuleID],[Status])
GO
CREATE INDEX [LookupProduct] ON [ShopProducts]([CatID],[Status],[ModuleID])
GO
CREATE INDEX [ImportID] ON [ShopProducts]([ImportID])
GO
CREATE TABLE [dbo].[Wholesale2b_Feeds](
	[FeedID] [float] NOT NULL,
	[FeedName] [nvarchar](100) NULL,
	[FeedURL] [nvarchar](2048) NULL,
	[DatePosted] [datetime] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
	[LastRunTime] [datetime] null,
 CONSTRAINT [PK_FeedID_Wholesale2b_Feeds] PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE INDEX [LastRunTime] ON [Wholesale2b_Feeds]([LastRunTime])
GO
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Wholesale2b_Feeds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE Blog ADD [CatID] [float]
GO
ALTER TABLE UPagesSites ADD [CatID] [float]
GO
ALTER TABLE News ADD [CatID] [float]
GO
CREATE INDEX [CatID] ON [News]([CatID])
GO
CREATE INDEX [CatID] ON [UPagesSites]([CatID])
GO
CREATE INDEX [CatID] ON [Blog]([CatID])
GO
UPDATE Blog SET [CatID]='0'
GO
UPDATE UPagesSites SET [CatID]='0'
GO
UPDATE News SET [CatID]='0'
GO
ALTER TABLE BG_Processes ADD [RecurringDays] [int]
GO
