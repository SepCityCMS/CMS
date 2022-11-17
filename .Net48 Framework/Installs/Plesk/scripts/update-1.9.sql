CREATE TABLE [dbo].[Geo](
	[CountryCode] [varchar](20) NULL,
	[PostalCode] [varchar] (180) NULL,
	[RegionName] [varchar] (100) NULL,
	[RegionCode] [nvarchar](20) NULL,
	[ProvinceName] [varchar] (100) NULL,
	[ProvinceCode] [varchar] (20) NULL,
	[CommunityName] [varchar] (100) NULL,
	[CommunityCode] [varchar] (20) NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Accuracy] [float] NOT NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CountryCode] ON [dbo].[Geo]
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [PostalCode] ON [dbo].[Geo]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [RegionName] ON [dbo].[Geo]
(
	[RegionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [RegionCode] ON [dbo].[Geo]
(
	[RegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProvinceName] ON [dbo].[Geo]
(
	[ProvinceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ProvinceCode] ON [dbo].[Geo]
(
	[ProvinceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CommunityName] ON [dbo].[Geo]
(
	[CommunityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [CommunityCode] ON [dbo].[Geo]
(
	[CommunityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Latitude] ON [dbo].[Geo]
(
	[Latitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Longitude] ON [dbo].[Geo]
(
	[Longitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Feeds](
	[FeedID] [Float] NOT NULL,
	[UniqueID] [Float] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [ntext] NULL,
	[UserID] [varchar](40) NULL,
	[MoreLink] [varchar](255) NULL,
	[DatePosted] [datetime] NULL,
 CONSTRAINT [PK_FeedID_Feeds] PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[FeedsUsers](
	[FeedID] [Float] NOT NULL,
	[UpVote] [int] NULL,
	[DownVote] [int] NULL,
	[UserID] [varchar](40) NULL,
	[Comment] [varchar](500) NULL,
	[isFavorite] [bit] NULL,
	[isDeleted] [bit] NULL,
	[DatePosted] [datetime] NULL
) ON [PRIMARY]
GO

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('978438673456744', '62', '62', 'Feeds', '<h1>My Feeds</h1>', '', '1', '', '1', 'my_feeds.aspx', '', '', '', '1', '100')
GO

CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Feeds]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Feeds]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Feeds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [FeedID] ON [dbo].[FeedsUsers]
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FeedsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [isFavorite] ON [dbo].[FeedsUsers]
(
	[isFavorite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [isDeleted] ON [dbo].[FeedsUsers]
(
	[isDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [Country] ON [dbo].[Advertisements]
(
	[Country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [State] ON [dbo].[Advertisements]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE Blog ADD StartDate [datetime]
GO
ALTER TABLE Blog ADD EndDate [datetime]
GO
ALTER TABLE News ADD StartDate [datetime]
GO
ALTER TABLE News ADD EndDate [datetime]
GO
ALTER TABLE Members ADD SiteID [float]
GO
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[Blog]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[Blog]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[News]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[News]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [SiteID] ON [dbo].[Members]
(
	[SiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
UPDATE Blog SET StartDate=GETDATE()
GO
UPDATE Blog SET EndDate='1/1/2099'
GO
UPDATE News SET StartDate=GETDATE()
GO
UPDATE News SET EndDate='1/1/2099'
GO
UPDATE Members SET SiteID='0'
GO
ALTER TABLE UPagesSites DROP COLUMN [Password]
GO
ALTER TABLE UPagesSites Add [ShowList] [bit]
GO
ALTER TABLE UPagesSites Add [InviteOnly] [bit]
GO
CREATE NONCLUSTERED INDEX [ShowList] ON [dbo].[UPagesSites]
(
	[ShowList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [InviteOnly] ON [dbo].[UPagesSites]
(
	[InviteOnly] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
UPDATE UPagesSites SET ShowList='1'
GO
UPDATE UPagesSites SET InviteOnly='0'
GO
ALTER TABLE UPagesPages Add [Password] [nvarchar] (50)
GO
CREATE NONCLUSTERED INDEX [Password] ON [dbo].[UPagesPages]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE Members ADD [Password2] [varchar] (100)
GO
UPDATE Members SET Password2=Password
GO
ALTER TABLE Members DROP COLUMN Password
GO
ALTER TABLE Members ADD [Password] [varchar] (100)
GO
UPDATE Members SET Password=Password2
GO
ALTER TABLE Members DROP COLUMN Password2
GO

UPDATE Invoices_Products SET AffiliateUnitPrice=Replace(UnitPrice,'$','')
GO
UPDATE Invoices_Products SET AffiliateRecurringPrice=Replace(RecurringPrice,'$','')
GO

ALTER TABLE ShopProducts ADD [UPC_Code] [varchar] (25)
GO
ALTER TABLE ShopProducts ADD [isRefurbished] [bit]
GO
ALTER TABLE ShopProducts ADD [ImportSource] [varchar] (50)
GO
ALTER TABLE ShopProducts ADD [ImportID] [varchar] (50)
GO
DECLARE @sql NVARCHAR(MAX);

SET @sql = N'';

SELECT @sql = @sql + N'ALTER TABLE ' 
  + QUOTENAME(OBJECT_SCHEMA_NAME([parent_object_id]))
  + '.' + QUOTENAME(OBJECT_NAME([parent_object_id])) 
  + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';
'
FROM sys.foreign_keys
WHERE OBJECTPROPERTY([parent_object_id], 'IsMsShipped') = 0;

EXEC sp_executesql @sql;

SET @sql = N'';

SELECT @sql = @sql + N'ALTER TABLE '
  + QUOTENAME(OBJECT_SCHEMA_NAME([parent_object_id]))
  + '.' + QUOTENAME(OBJECT_NAME([parent_object_id])) 
  + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';
'
FROM sys.key_constraints 
WHERE [type] = 'PK'
AND OBJECTPROPERTY([parent_object_id], 'IsMsShipped') = 0;

EXEC sp_executesql @sql;

SET @sql = N'';

SELECT @sql = @sql + N'DROP INDEX ' 
  + QUOTENAME(name) + ' ON '
  + QUOTENAME(OBJECT_SCHEMA_NAME([object_id]))
  + '.' + QUOTENAME(OBJECT_NAME([object_id])) + ';
'
FROM sys.indexes 
WHERE index_id > 0
AND OBJECTPROPERTY([object_id], 'IsMsShipped') = 0
ORDER BY [object_id], index_id DESC;

EXEC sp_executesql @sql;

insert into PortalPages 
(PageID, LinkText, PageText, Description, MenuID, Keywords, Weight, UserPageName, TargetWindow, Visits, PageTitle, ViewPage, PortalIDs, PortalID, Status, UniqueID) 
SELECT '62', 'My Feeds', '<h1>My Feeds</h1>', '', '3', '', '0', 'my_feeds.aspx', '_parent', '0', 'My Feeds', '0', '', Portals.PortalID, '1', '566776' + Portals.PortalID FROM Portals
GO
insert into PortalPages (UniqueID, PortalID,PageID,LinkText,PageText,Description,MenuID,Keywords,Weight,UserPageName,TargetWindow, Status) VALUES ('36457667','0', '62', 'My Feeds', '<h1>My Feeds</h1>', '', '3', '', '100',  'my_feeds.aspx', '', '1')
GO
ALTER TABLE AccessClasses ADD PortalIDs2 [varchar](max)
GO
UPDATE AccessClasses SET PortalIDs2=PortalIDs
GO
ALTER TABLE AccessClasses DROP COLUMN PortalIDs
GO
ALTER TABLE AccessClasses ADD PortalIDs [varchar](max)
GO
UPDATE AccessClasses SET PortalIDs=PortalIDs2
GO
ALTER TABLE AccessClasses DROP COLUMN PortalIDs2
GO
ALTER TABLE Advertisements ADD PortalIDs2 [varchar](max)
GO
UPDATE Advertisements SET PortalIDs2=PortalIDs
GO
ALTER TABLE Advertisements DROP COLUMN PortalIDs
GO
ALTER TABLE Advertisements ADD PortalIDs [varchar](max)
GO
UPDATE Advertisements SET PortalIDs=PortalIDs2
GO
ALTER TABLE Advertisements DROP COLUMN PortalIDs2
GO
ALTER TABLE Approvals ADD PortalIDs2 [varchar](max)
GO
UPDATE Approvals SET PortalIDs2=PortalIDs
GO
ALTER TABLE Approvals DROP COLUMN PortalIDs
GO
ALTER TABLE Approvals ADD PortalIDs [varchar](max)
GO
UPDATE Approvals SET PortalIDs=PortalIDs2
GO
ALTER TABLE Approvals DROP COLUMN PortalIDs2
GO
ALTER TABLE ContentRotator ADD PortalIDs2 [varchar](max)
GO
UPDATE ContentRotator SET PortalIDs2=PortalIDs
GO
ALTER TABLE ContentRotator DROP COLUMN PortalIDs
GO
ALTER TABLE ContentRotator ADD PortalIDs [varchar](max)
GO
UPDATE ContentRotator SET PortalIDs=PortalIDs2
GO
ALTER TABLE ContentRotator DROP COLUMN PortalIDs2
GO
ALTER TABLE CustomFields ADD PortalIDs2 [varchar](max)
GO
UPDATE CustomFields SET PortalIDs2=PortalIDs
GO
ALTER TABLE CustomFields DROP COLUMN PortalIDs
GO
ALTER TABLE CustomFields ADD PortalIDs [varchar](max)
GO
UPDATE CustomFields SET PortalIDs=PortalIDs2
GO
ALTER TABLE CustomFields DROP COLUMN PortalIDs2
GO
ALTER TABLE LiveChatUsers ADD PortalIDs2 [varchar](max)
GO
UPDATE LiveChatUsers SET PortalIDs2=PortalIDs
GO
ALTER TABLE LiveChatUsers DROP COLUMN PortalIDs
GO
ALTER TABLE LiveChatUsers ADD PortalIDs [varchar](max)
GO
UPDATE LiveChatUsers SET PortalIDs=PortalIDs2
GO
ALTER TABLE LiveChatUsers DROP COLUMN PortalIDs2
GO
ALTER TABLE Newsletters ADD PortalIDs2 [varchar](max)
GO
UPDATE Newsletters SET PortalIDs2=PortalIDs
GO
ALTER TABLE Newsletters DROP COLUMN PortalIDs
GO
ALTER TABLE Newsletters ADD PortalIDs [varchar](max)
GO
UPDATE Newsletters SET PortalIDs=PortalIDs2
GO
ALTER TABLE Newsletters DROP COLUMN PortalIDs2
GO
ALTER TABLE PortalPages ADD PortalIDs2 [varchar](max)
GO
UPDATE PortalPages SET PortalIDs2=PortalIDs
GO
ALTER TABLE PortalPages DROP COLUMN PortalIDs
GO
ALTER TABLE PortalPages ADD PortalIDs [varchar](max)
GO
UPDATE PortalPages SET PortalIDs=PortalIDs2
GO
ALTER TABLE PortalPages DROP COLUMN PortalIDs2
GO
ALTER TABLE Scripts ADD PortalIDs2 [varchar](max)
GO
UPDATE Scripts SET PortalIDs2=PortalIDs
GO
ALTER TABLE Scripts DROP COLUMN PortalIDs
GO
ALTER TABLE Scripts ADD PortalIDs [varchar](max)
GO
UPDATE Scripts SET PortalIDs=PortalIDs2
GO
ALTER TABLE Scripts DROP COLUMN PortalIDs2
GO
ALTER TABLE Approvals ADD ModuleIDs2 [varchar](max)
GO
UPDATE Approvals SET ModuleIDs2=ModuleIDs
GO
ALTER TABLE Approvals DROP COLUMN ModuleIDs
GO
ALTER TABLE Approvals ADD ModuleIDs [varchar](max)
GO
UPDATE Approvals SET ModuleIDs=ModuleIDs2
GO
ALTER TABLE Approvals DROP COLUMN ModuleIDs2
GO
ALTER TABLE CustomFields ADD ModuleIDs2 [varchar](max)
GO
UPDATE CustomFields SET ModuleIDs2=ModuleIDs
GO
ALTER TABLE CustomFields DROP COLUMN ModuleIDs
GO
ALTER TABLE CustomFields ADD ModuleIDs [varchar](max)
GO
UPDATE CustomFields SET ModuleIDs=ModuleIDs2
GO
ALTER TABLE CustomFields DROP COLUMN ModuleIDs2
GO
ALTER TABLE Reviews ADD ModuleIDs2 [varchar](max)
GO
UPDATE Reviews SET ModuleIDs2=ModuleIDs
GO
ALTER TABLE Reviews DROP COLUMN ModuleIDs
GO
ALTER TABLE Reviews ADD ModuleIDs [varchar](max)
GO
UPDATE Reviews SET ModuleIDs=ModuleIDs2
GO
ALTER TABLE Reviews DROP COLUMN ModuleIDs2
GO
ALTER TABLE Scripts ADD ModuleIDs2 [varchar](max)
GO
UPDATE Scripts SET ModuleIDs2=ModuleIDs
GO
ALTER TABLE Scripts DROP COLUMN ModuleIDs
GO
ALTER TABLE Scripts ADD ModuleIDs [varchar](max)
GO
UPDATE Scripts SET ModuleIDs=ModuleIDs2
GO
ALTER TABLE Scripts DROP COLUMN ModuleIDs2
GO
ALTER TABLE Activities ADD UserID2 [varchar](40)
GO
UPDATE Activities SET UserID2=UserID
GO
ALTER TABLE Activities DROP COLUMN UserID
GO
ALTER TABLE Activities ADD UserID [varchar](40)
GO
UPDATE Activities SET UserID=UserID2
GO
ALTER TABLE Activities DROP COLUMN UserID2
GO
ALTER TABLE Advertisements ADD UserID2 [varchar](40)
GO
UPDATE Advertisements SET UserID2=UserID
GO
ALTER TABLE Advertisements DROP COLUMN UserID
GO
ALTER TABLE Advertisements ADD UserID [varchar](40)
GO
UPDATE Advertisements SET UserID=UserID2
GO
ALTER TABLE Advertisements DROP COLUMN UserID2
GO
ALTER TABLE Articles ADD UserID2 [varchar](40)
GO
UPDATE Articles SET UserID2=UserID
GO
ALTER TABLE Articles DROP COLUMN UserID
GO
ALTER TABLE Articles ADD UserID [varchar](40)
GO
UPDATE Articles SET UserID=UserID2
GO
ALTER TABLE Articles DROP COLUMN UserID2
GO
ALTER TABLE AuctionAds ADD UserID2 [varchar](40)
GO
UPDATE AuctionAds SET UserID2=UserID
GO
ALTER TABLE AuctionAds DROP COLUMN UserID
GO
ALTER TABLE AuctionAds ADD UserID [varchar](40)
GO
UPDATE AuctionAds SET UserID=UserID2
GO
ALTER TABLE AuctionAds DROP COLUMN UserID2
GO
ALTER TABLE Blog ADD UserID2 [varchar](40)
GO
UPDATE Blog SET UserID2=UserID
GO
ALTER TABLE Blog DROP COLUMN UserID
GO
ALTER TABLE Blog ADD UserID [varchar](40)
GO
UPDATE Blog SET UserID=UserID2
GO
ALTER TABLE Blog DROP COLUMN UserID2
GO
ALTER TABLE BusinessListings ADD UserID2 [varchar](40)
GO
UPDATE BusinessListings SET UserID2=UserID
GO
ALTER TABLE BusinessListings DROP COLUMN UserID
GO
ALTER TABLE BusinessListings ADD UserID [varchar](40)
GO
UPDATE BusinessListings SET UserID=UserID2
GO
ALTER TABLE BusinessListings DROP COLUMN UserID2
GO
ALTER TABLE ClassifiedsAds ADD UserID2 [varchar](40)
GO
UPDATE ClassifiedsAds SET UserID2=UserID
GO
ALTER TABLE ClassifiedsAds DROP COLUMN UserID
GO
ALTER TABLE ClassifiedsAds ADD UserID [varchar](40)
GO
UPDATE ClassifiedsAds SET UserID=UserID2
GO
ALTER TABLE ClassifiedsAds DROP COLUMN UserID2
GO
ALTER TABLE Comments ADD UserID2 [varchar](40)
GO
UPDATE Comments SET UserID2=UserID
GO
ALTER TABLE Comments DROP COLUMN UserID
GO
ALTER TABLE Comments ADD UserID [varchar](40)
GO
UPDATE Comments SET UserID=UserID2
GO
ALTER TABLE Comments DROP COLUMN UserID2
GO
ALTER TABLE CustomFieldUsers ADD UserID2 [varchar](40)
GO
UPDATE CustomFieldUsers SET UserID2=UserID
GO
ALTER TABLE CustomFieldUsers DROP COLUMN UserID
GO
ALTER TABLE CustomFieldUsers ADD UserID [varchar](40)
GO
UPDATE CustomFieldUsers SET UserID=UserID2
GO
ALTER TABLE CustomFieldUsers DROP COLUMN UserID2
GO
ALTER TABLE DiscountSystem ADD UserID2 [varchar](40)
GO
UPDATE DiscountSystem SET UserID2=UserID
GO
ALTER TABLE DiscountSystem DROP COLUMN UserID
GO
ALTER TABLE DiscountSystem ADD UserID [varchar](40)
GO
UPDATE DiscountSystem SET UserID=UserID2
GO
ALTER TABLE DiscountSystem DROP COLUMN UserID2
GO
ALTER TABLE ELearnExamGrades ADD UserID2 [varchar](40)
GO
UPDATE ELearnExamGrades SET UserID2=UserID
GO
ALTER TABLE ELearnExamGrades DROP COLUMN UserID
GO
ALTER TABLE ELearnExamGrades ADD UserID [varchar](40)
GO
UPDATE ELearnExamGrades SET UserID=UserID2
GO
ALTER TABLE ELearnExamGrades DROP COLUMN UserID2
GO
ALTER TABLE ELearnExamUsers ADD UserID2 [varchar](40)
GO
UPDATE ELearnExamUsers SET UserID2=UserID
GO
ALTER TABLE ELearnExamUsers DROP COLUMN UserID
GO
ALTER TABLE ELearnExamUsers ADD UserID [varchar](40)
GO
UPDATE ELearnExamUsers SET UserID=UserID2
GO
ALTER TABLE ELearnExamUsers DROP COLUMN UserID2
GO
ALTER TABLE ELearnHomeUsers ADD UserID2 [varchar](40)
GO
UPDATE ELearnHomeUsers SET UserID2=UserID
GO
ALTER TABLE ELearnHomeUsers DROP COLUMN UserID
GO
ALTER TABLE ELearnHomeUsers ADD UserID [varchar](40)
GO
UPDATE ELearnHomeUsers SET UserID=UserID2
GO
ALTER TABLE ELearnHomeUsers DROP COLUMN UserID2
GO
ALTER TABLE ELearnStudents ADD UserID2 [varchar](40)
GO
UPDATE ELearnStudents SET UserID2=UserID
GO
ALTER TABLE ELearnStudents DROP COLUMN UserID
GO
ALTER TABLE ELearnStudents ADD UserID [varchar](40)
GO
UPDATE ELearnStudents SET UserID=UserID2
GO
ALTER TABLE ELearnStudents DROP COLUMN UserID2
GO
ALTER TABLE EventCalendar ADD UserID2 [varchar](40)
GO
UPDATE EventCalendar SET UserID2=UserID
GO
ALTER TABLE EventCalendar DROP COLUMN UserID
GO
ALTER TABLE EventCalendar ADD UserID [varchar](40)
GO
UPDATE EventCalendar SET UserID=UserID2
GO
ALTER TABLE EventCalendar DROP COLUMN UserID2
GO
ALTER TABLE Favorites ADD UserID2 [varchar](40)
GO
UPDATE Favorites SET UserID2=UserID
GO
ALTER TABLE Favorites DROP COLUMN UserID
GO
ALTER TABLE Favorites ADD UserID [varchar](40)
GO
UPDATE Favorites SET UserID=UserID2
GO
ALTER TABLE Favorites DROP COLUMN UserID2
GO
ALTER TABLE FormAnswers ADD UserID2 [varchar](40)
GO
UPDATE FormAnswers SET UserID2=UserID
GO
ALTER TABLE FormAnswers DROP COLUMN UserID
GO
ALTER TABLE FormAnswers ADD UserID [varchar](40)
GO
UPDATE FormAnswers SET UserID=UserID2
GO
ALTER TABLE FormAnswers DROP COLUMN UserID2
GO
ALTER TABLE ForumsMessages ADD UserID2 [varchar](40)
GO
UPDATE ForumsMessages SET UserID2=UserID
GO
ALTER TABLE ForumsMessages DROP COLUMN UserID
GO
ALTER TABLE ForumsMessages ADD UserID [varchar](40)
GO
UPDATE ForumsMessages SET UserID=UserID2
GO
ALTER TABLE ForumsMessages DROP COLUMN UserID2
GO
ALTER TABLE FriendsList ADD UserID2 [varchar](40)
GO
UPDATE FriendsList SET UserID2=UserID
GO
ALTER TABLE FriendsList DROP COLUMN UserID
GO
ALTER TABLE FriendsList ADD UserID [varchar](40)
GO
UPDATE FriendsList SET UserID=UserID2
GO
ALTER TABLE FriendsList DROP COLUMN UserID2
GO
ALTER TABLE GalleryPhotos ADD UserID2 [varchar](40)
GO
UPDATE GalleryPhotos SET UserID2=UserID
GO
ALTER TABLE GalleryPhotos DROP COLUMN UserID
GO
ALTER TABLE GalleryPhotos ADD UserID [varchar](40)
GO
UPDATE GalleryPhotos SET UserID=UserID2
GO
ALTER TABLE GalleryPhotos DROP COLUMN UserID2
GO
ALTER TABLE GroupLists ADD UserID2 [varchar](40)
GO
UPDATE GroupLists SET UserID2=UserID
GO
ALTER TABLE GroupLists DROP COLUMN UserID
GO
ALTER TABLE GroupLists ADD UserID [varchar](40)
GO
UPDATE GroupLists SET UserID=UserID2
GO
ALTER TABLE GroupLists DROP COLUMN UserID2
GO
ALTER TABLE GroupListsUsers ADD UserID2 [varchar](40)
GO
UPDATE GroupListsUsers SET UserID2=UserID
GO
ALTER TABLE GroupListsUsers DROP COLUMN UserID
GO
ALTER TABLE GroupListsUsers ADD UserID [varchar](40)
GO
UPDATE GroupListsUsers SET UserID=UserID2
GO
ALTER TABLE GroupListsUsers DROP COLUMN UserID2
GO
ALTER TABLE Guestbook ADD UserID2 [varchar](40)
GO
UPDATE Guestbook SET UserID2=UserID
GO
ALTER TABLE Guestbook DROP COLUMN UserID
GO
ALTER TABLE Guestbook ADD UserID [varchar](40)
GO
UPDATE Guestbook SET UserID=UserID2
GO
ALTER TABLE Guestbook DROP COLUMN UserID2
GO
ALTER TABLE Invoices ADD UserID2 [varchar](40)
GO
UPDATE Invoices SET UserID2=UserID
GO
ALTER TABLE Invoices DROP COLUMN UserID
GO
ALTER TABLE Invoices ADD UserID [varchar](40)
GO
UPDATE Invoices SET UserID=UserID2
GO
ALTER TABLE Invoices DROP COLUMN UserID2
GO
ALTER TABLE JobListCompanies ADD UserID2 [varchar](40)
GO
UPDATE JobListCompanies SET UserID2=UserID
GO
ALTER TABLE JobListCompanies DROP COLUMN UserID
GO
ALTER TABLE JobListCompanies ADD UserID [varchar](40)
GO
UPDATE JobListCompanies SET UserID=UserID2
GO
ALTER TABLE JobListCompanies DROP COLUMN UserID2
GO
ALTER TABLE JobListings ADD UserID2 [varchar](40)
GO
UPDATE JobListings SET UserID2=UserID
GO
ALTER TABLE JobListings DROP COLUMN UserID
GO
ALTER TABLE JobListings ADD UserID [varchar](40)
GO
UPDATE JobListings SET UserID=UserID2
GO
ALTER TABLE JobListings DROP COLUMN UserID2
GO
ALTER TABLE JobListResumes ADD UserID2 [varchar](40)
GO
UPDATE JobListResumes SET UserID2=UserID
GO
ALTER TABLE JobListResumes DROP COLUMN UserID
GO
ALTER TABLE JobListResumes ADD UserID [varchar](40)
GO
UPDATE JobListResumes SET UserID=UserID2
GO
ALTER TABLE JobListResumes DROP COLUMN UserID2
GO
ALTER TABLE LibrariesFiles ADD UserID2 [varchar](40)
GO
UPDATE LibrariesFiles SET UserID2=UserID
GO
ALTER TABLE LibrariesFiles DROP COLUMN UserID
GO
ALTER TABLE LibrariesFiles ADD UserID [varchar](40)
GO
UPDATE LibrariesFiles SET UserID=UserID2
GO
ALTER TABLE LibrariesFiles DROP COLUMN UserID2
GO
ALTER TABLE LinksWebSites ADD UserID2 [varchar](40)
GO
UPDATE LinksWebSites SET UserID2=UserID
GO
ALTER TABLE LinksWebSites DROP COLUMN UserID
GO
ALTER TABLE LinksWebSites ADD UserID [varchar](40)
GO
UPDATE LinksWebSites SET UserID=UserID2
GO
ALTER TABLE LinksWebSites DROP COLUMN UserID2
GO
ALTER TABLE LiveChat ADD UserID2 [varchar](40)
GO
UPDATE LiveChat SET UserID2=UserID
GO
ALTER TABLE LiveChat DROP COLUMN UserID
GO
ALTER TABLE LiveChat ADD UserID [varchar](40)
GO
UPDATE LiveChat SET UserID=UserID2
GO
ALTER TABLE LiveChat DROP COLUMN UserID2
GO
ALTER TABLE LiveChatLogs ADD UserID2 [varchar](40)
GO
UPDATE LiveChatLogs SET UserID2=UserID
GO
ALTER TABLE LiveChatLogs DROP COLUMN UserID
GO
ALTER TABLE LiveChatLogs ADD UserID [varchar](40)
GO
UPDATE LiveChatLogs SET UserID=UserID2
GO
ALTER TABLE LiveChatLogs DROP COLUMN UserID2
GO
ALTER TABLE LiveChatUsers ADD UserID2 [varchar](40)
GO
UPDATE LiveChatUsers SET UserID2=UserID
GO
ALTER TABLE LiveChatUsers DROP COLUMN UserID
GO
ALTER TABLE LiveChatUsers ADD UserID [varchar](40)
GO
UPDATE LiveChatUsers SET UserID=UserID2
GO
ALTER TABLE LiveChatUsers DROP COLUMN UserID2
GO
ALTER TABLE MatchMaker ADD UserID2 [varchar](40)
GO
UPDATE MatchMaker SET UserID2=UserID
GO
ALTER TABLE MatchMaker DROP COLUMN UserID
GO
ALTER TABLE MatchMaker ADD UserID [varchar](40)
GO
UPDATE MatchMaker SET UserID=UserID2
GO
ALTER TABLE MatchMaker DROP COLUMN UserID2
GO
ALTER TABLE Members ADD UserID2 [varchar](40)
GO
UPDATE Members SET UserID2=UserID
GO
ALTER TABLE Members DROP COLUMN UserID
GO
ALTER TABLE Members ADD UserID [varchar](40)
GO
UPDATE Members SET UserID=UserID2
GO
ALTER TABLE Members DROP COLUMN UserID2
GO
ALTER TABLE MessengerBlocked ADD UserID2 [varchar](40)
GO
UPDATE MessengerBlocked SET UserID2=UserID
GO
ALTER TABLE MessengerBlocked DROP COLUMN UserID
GO
ALTER TABLE MessengerBlocked ADD UserID [varchar](40)
GO
UPDATE MessengerBlocked SET UserID=UserID2
GO
ALTER TABLE MessengerBlocked DROP COLUMN UserID2
GO
ALTER TABLE NewslettersUsers ADD UserID2 [varchar](40)
GO
UPDATE NewslettersUsers SET UserID2=UserID
GO
ALTER TABLE NewslettersUsers DROP COLUMN UserID
GO
ALTER TABLE NewslettersUsers ADD UserID [varchar](40)
GO
UPDATE NewslettersUsers SET UserID=UserID2
GO
ALTER TABLE NewslettersUsers DROP COLUMN UserID2
GO
ALTER TABLE OnlineUsers ADD UserID2 [varchar](40)
GO
UPDATE OnlineUsers SET UserID2=UserID
GO
ALTER TABLE OnlineUsers DROP COLUMN UserID
GO
ALTER TABLE OnlineUsers ADD UserID [varchar](40)
GO
UPDATE OnlineUsers SET UserID=UserID2
GO
ALTER TABLE OnlineUsers DROP COLUMN UserID2
GO
ALTER TABLE PhotoAlbums ADD UserID2 [varchar](40)
GO
UPDATE PhotoAlbums SET UserID2=UserID
GO
ALTER TABLE PhotoAlbums DROP COLUMN UserID
GO
ALTER TABLE PhotoAlbums ADD UserID [varchar](40)
GO
UPDATE PhotoAlbums SET UserID=UserID2
GO
ALTER TABLE PhotoAlbums DROP COLUMN UserID2
GO
ALTER TABLE Portals ADD UserID2 [varchar](40)
GO
UPDATE Portals SET UserID2=UserID
GO
ALTER TABLE Portals DROP COLUMN UserID
GO
ALTER TABLE Portals ADD UserID [varchar](40)
GO
UPDATE Portals SET UserID=UserID2
GO
ALTER TABLE Portals DROP COLUMN UserID2
GO
ALTER TABLE Profiles ADD UserID2 [varchar](40)
GO
UPDATE Profiles SET UserID2=UserID
GO
ALTER TABLE Profiles DROP COLUMN UserID
GO
ALTER TABLE Profiles ADD UserID [varchar](40)
GO
UPDATE Profiles SET UserID=UserID2
GO
ALTER TABLE Profiles DROP COLUMN UserID2
GO
ALTER TABLE Ratings ADD UserID2 [varchar](40)
GO
UPDATE Ratings SET UserID2=UserID
GO
ALTER TABLE Ratings DROP COLUMN UserID
GO
ALTER TABLE Ratings ADD UserID [varchar](40)
GO
UPDATE Ratings SET UserID=UserID2
GO
ALTER TABLE Ratings DROP COLUMN UserID2
GO
ALTER TABLE ReviewsUsers ADD UserID2 [varchar](40)
GO
UPDATE ReviewsUsers SET UserID2=UserID
GO
ALTER TABLE ReviewsUsers DROP COLUMN UserID
GO
ALTER TABLE ReviewsUsers ADD UserID [varchar](40)
GO
UPDATE ReviewsUsers SET UserID=UserID2
GO
ALTER TABLE ReviewsUsers DROP COLUMN UserID2
GO
ALTER TABLE RStateAgents ADD UserID2 [varchar](40)
GO
UPDATE RStateAgents SET UserID2=UserID
GO
ALTER TABLE RStateAgents DROP COLUMN UserID
GO
ALTER TABLE RStateAgents ADD UserID [varchar](40)
GO
UPDATE RStateAgents SET UserID=UserID2
GO
ALTER TABLE RStateAgents DROP COLUMN UserID2
GO
ALTER TABLE RStateBrokers ADD UserID2 [varchar](40)
GO
UPDATE RStateBrokers SET UserID2=UserID
GO
ALTER TABLE RStateBrokers DROP COLUMN UserID
GO
ALTER TABLE RStateBrokers ADD UserID [varchar](40)
GO
UPDATE RStateBrokers SET UserID=UserID2
GO
ALTER TABLE RStateBrokers DROP COLUMN UserID2
GO
ALTER TABLE RStateProperty ADD UserID2 [varchar](40)
GO
UPDATE RStateProperty SET UserID2=UserID
GO
ALTER TABLE RStateProperty DROP COLUMN UserID
GO
ALTER TABLE RStateProperty ADD UserID [varchar](40)
GO
UPDATE RStateProperty SET UserID=UserID2
GO
ALTER TABLE RStateProperty DROP COLUMN UserID2
GO
ALTER TABLE Scripts ADD UserID2 [varchar](40)
GO
UPDATE Scripts SET UserID2=UserID
GO
ALTER TABLE Scripts DROP COLUMN UserID
GO
ALTER TABLE Scripts ADD UserID [varchar](40)
GO
UPDATE Scripts SET UserID=UserID2
GO
ALTER TABLE Scripts DROP COLUMN UserID2
GO
ALTER TABLE Stocks ADD UserID2 [varchar](40)
GO
UPDATE Stocks SET UserID2=UserID
GO
ALTER TABLE Stocks DROP COLUMN UserID
GO
ALTER TABLE Stocks ADD UserID [varchar](40)
GO
UPDATE Stocks SET UserID=UserID2
GO
ALTER TABLE Stocks DROP COLUMN UserID2
GO
ALTER TABLE UPagesGuestbook ADD UserID2 [varchar](40)
GO
UPDATE UPagesGuestbook SET UserID2=UserID
GO
ALTER TABLE UPagesGuestbook DROP COLUMN UserID
GO
ALTER TABLE UPagesGuestbook ADD UserID [varchar](40)
GO
UPDATE UPagesGuestbook SET UserID=UserID2
GO
ALTER TABLE UPagesGuestbook DROP COLUMN UserID2
GO
ALTER TABLE UPagesPages ADD UserID2 [varchar](40)
GO
UPDATE UPagesPages SET UserID2=UserID
GO
ALTER TABLE UPagesPages DROP COLUMN UserID
GO
ALTER TABLE UPagesPages ADD UserID [varchar](40)
GO
UPDATE UPagesPages SET UserID=UserID2
GO
ALTER TABLE UPagesPages DROP COLUMN UserID2
GO
ALTER TABLE UPagesSites ADD UserID2 [varchar](40)
GO
UPDATE UPagesSites SET UserID2=UserID
GO
ALTER TABLE UPagesSites DROP COLUMN UserID
GO
ALTER TABLE UPagesSites ADD UserID [varchar](40)
GO
UPDATE UPagesSites SET UserID=UserID2
GO
ALTER TABLE UPagesSites DROP COLUMN UserID2
GO
ALTER TABLE Uploads ADD UserID2 [varchar](40)
GO
UPDATE Uploads SET UserID2=UserID
GO
ALTER TABLE Uploads DROP COLUMN UserID
GO
ALTER TABLE Uploads ADD UserID [varchar](40)
GO
UPDATE Uploads SET UserID=UserID2
GO
ALTER TABLE Uploads DROP COLUMN UserID2
GO
ALTER TABLE UserReviewAnswers ADD UserID2 [varchar](40)
GO
UPDATE UserReviewAnswers SET UserID2=UserID
GO
ALTER TABLE UserReviewAnswers DROP COLUMN UserID
GO
ALTER TABLE UserReviewAnswers ADD UserID [varchar](40)
GO
UPDATE UserReviewAnswers SET UserID=UserID2
GO
ALTER TABLE UserReviewAnswers DROP COLUMN UserID2
GO
ALTER TABLE Vouchers ADD UserID2 [varchar](40)
GO
UPDATE Vouchers SET UserID2=UserID
GO
ALTER TABLE Vouchers DROP COLUMN UserID
GO
ALTER TABLE Vouchers ADD UserID [varchar](40)
GO
UPDATE Vouchers SET UserID=UserID2
GO
ALTER TABLE Vouchers DROP COLUMN UserID2
GO
ALTER TABLE VouchersPurchased ADD UserID2 [varchar](40)
GO
UPDATE VouchersPurchased SET UserID2=UserID
GO
ALTER TABLE VouchersPurchased DROP COLUMN UserID
GO
ALTER TABLE VouchersPurchased ADD UserID [varchar](40)
GO
UPDATE VouchersPurchased SET UserID=UserID2
GO
ALTER TABLE VouchersPurchased DROP COLUMN UserID2
GO
alter table AccessClasses alter column KeyIDs varchar(max) null
GO
alter table AccessClasses alter column LoggedSwitchTo varchar(40) null
GO
alter table AccessClasses alter column InSwitchTo varchar(40) null
GO
alter table Activities alter column IPAddress varchar(50) null
GO
alter table Advertisements alter column ImageURL varchar(255) null
GO
alter table Advertisements alter column SiteURL varchar(255) null
GO
alter table Advertisements alter column CatIDs varchar(max) null
GO
alter table Advertisements alter column PageIDs varchar(max) null
GO
UPDATE AffiliatePaid SET AmountPaid=CAST(CASE WHEN ISNUMERIC(AmountPaid + 'e0') = 1 THEN AmountPaid ELSE '0' END AS decimal(38, 10))
GO
alter table AffiliatePaid alter column AmountPaid [decimal](18, 0) null
GO
alter table ApprovalEmails alter column ApproveID varchar(40) null
GO
alter table ApprovalXML alter column ApproveID varchar(40) null
GO
alter table ApprovalXML alter column UniqueID varchar(40) null
GO
alter table Articles alter column Article_URL varchar(255) null
GO
alter table Articles alter column Source varchar(255) null
GO
UPDATE AuctionAds SET StartBid=CAST(CASE WHEN ISNUMERIC(StartBid + 'e0') = 1 THEN StartBid ELSE '0' END AS decimal(38, 10))
GO
alter table AuctionAds alter column StartBid [decimal](18, 0) null
GO
UPDATE AuctionAds SET CurrentBid=CAST(CASE WHEN ISNUMERIC(CurrentBid + 'e0') = 1 THEN CurrentBid ELSE '0' END AS decimal(38, 10))
GO
alter table AuctionAds alter column CurrentBid [decimal](18, 0) null
GO
UPDATE AuctionAds SET BidIncrease=CAST(CASE WHEN ISNUMERIC(BidIncrease + 'e0') = 1 THEN BidIncrease ELSE '0' END AS decimal(38, 10))
GO
alter table AuctionAds alter column BidIncrease [decimal](18, 0) null
GO
UPDATE AuctionAds SET MaxBid=CAST(CASE WHEN ISNUMERIC(MaxBid + 'e0') = 1 THEN MaxBid ELSE '0' END AS decimal(38, 10))
GO
alter table AuctionAds alter column MaxBid [decimal](18, 0) null
GO
alter table AuctionAds alter column BidUserID [varchar](40) null
GO
alter table AuctionFeedback alter column FromUserID [varchar](40) null
GO
alter table AuctionFeedback alter column ToUserID [varchar](40) null
GO
UPDATE [Invoices_Products] SET [UnitPrice]='0' WHERE [UnitPrice] IS NULL
GO
UPDATE [Invoices_Products] SET [UnitPrice]=CASE WHEN ISNUMERIC(Replace([UnitPrice],'$','')) = 1 THEN Replace([UnitPrice],'$','') ELSE '0' END
GO
UPDATE [Invoices_Products] SET [UnitPrice]=CAST(CASE WHEN ISNUMERIC([UnitPrice] + 'e0') = 1 THEN [UnitPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Invoices_Products] ALTER COLUMN [UnitPrice] [decimal] (18,0) NULL
GO

UPDATE [Invoices_Products] SET [RecurringPrice]='0' WHERE [RecurringPrice] IS NULL
GO
UPDATE [Invoices_Products] SET [RecurringPrice]=CASE WHEN ISNUMERIC(Replace([RecurringPrice],'$','')) = 1 THEN Replace([RecurringPrice],'$','') ELSE '0' END
GO
UPDATE [Invoices_Products] SET [RecurringPrice]=CAST(CASE WHEN ISNUMERIC([RecurringPrice] + 'e0') = 1 THEN [RecurringPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Invoices_Products] ALTER COLUMN [RecurringPrice] [decimal] (18,0) NULL
GO

UPDATE [Invoices_Products] SET [AffiliateUnitPrice]='0' WHERE [AffiliateUnitPrice] IS NULL
GO
UPDATE [Invoices_Products] SET [AffiliateUnitPrice]=CASE WHEN ISNUMERIC(Replace([AffiliateUnitPrice],'$','')) = 1 THEN Replace([AffiliateUnitPrice],'$','') ELSE '0' END
GO
UPDATE [Invoices_Products] SET [AffiliateUnitPrice]=CAST(CASE WHEN ISNUMERIC([AffiliateUnitPrice] + 'e0') = 1 THEN [AffiliateUnitPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Invoices_Products] ALTER COLUMN [AffiliateUnitPrice] [decimal] (18,0) NULL
GO

UPDATE [Invoices_Products] SET [AffiliateRecurringPrice]='0' WHERE [AffiliateRecurringPrice] IS NULL
GO
UPDATE [Invoices_Products] SET [AffiliateRecurringPrice]=CASE WHEN ISNUMERIC(Replace([AffiliateRecurringPrice],'$','')) = 1 THEN Replace([AffiliateRecurringPrice],'$','') ELSE '0' END
GO
UPDATE [Invoices_Products] SET [AffiliateRecurringPrice]=CAST(CASE WHEN ISNUMERIC([AffiliateRecurringPrice] + 'e0') = 1 THEN [AffiliateRecurringPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Invoices_Products] ALTER COLUMN [AffiliateRecurringPrice] [decimal] (18,0) NULL
GO

UPDATE [Invoices_Products] SET [Handling]='0' WHERE [Handling] IS NULL
GO
UPDATE [Invoices_Products] SET [Handling]=CASE WHEN ISNUMERIC(Replace([Handling],'$','')) = 1 THEN Replace([Handling],'$','') ELSE '0' END
GO
UPDATE [Invoices_Products] SET [Handling]=CAST(CASE WHEN ISNUMERIC([Handling] + 'e0') = 1 THEN [Handling] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Invoices_Products] ALTER COLUMN [Handling] [decimal] (18,0) NULL
GO

UPDATE [Vouchers] SET [RegularPrice]='0' WHERE [RegularPrice] IS NULL
GO
UPDATE [Vouchers] SET [RegularPrice]=CASE WHEN ISNUMERIC(Replace([RegularPrice],'$','')) = 1 THEN Replace([RegularPrice],'$','') ELSE '0' END
GO
UPDATE [Vouchers] SET [RegularPrice]=CAST(CASE WHEN ISNUMERIC([RegularPrice] + 'e0') = 1 THEN [RegularPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Vouchers] ALTER COLUMN [RegularPrice] [decimal] (18,0) NULL
GO

UPDATE [Vouchers] SET [SalePrice]='0' WHERE [SalePrice] IS NULL
GO
UPDATE [Vouchers] SET [SalePrice]=CASE WHEN ISNUMERIC(Replace([SalePrice],'$','')) = 1 THEN Replace([SalePrice],'$','') ELSE '0' END
GO
UPDATE [Vouchers] SET [SalePrice]=CAST(CASE WHEN ISNUMERIC([SalePrice] + 'e0') = 1 THEN [SalePrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[Vouchers] ALTER COLUMN [SalePrice] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [AffiliateUnitPrice]='0' WHERE [AffiliateUnitPrice] IS NULL
GO
UPDATE [ShopProducts] SET [AffiliateUnitPrice]=CASE WHEN ISNUMERIC(Replace([AffiliateUnitPrice],'$','')) = 1 THEN Replace([AffiliateUnitPrice],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [AffiliateUnitPrice]=CAST(CASE WHEN ISNUMERIC([AffiliateUnitPrice] + 'e0') = 1 THEN [AffiliateUnitPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [AffiliateUnitPrice] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [UnitPrice]='0' WHERE [UnitPrice] IS NULL
GO
UPDATE [ShopProducts] SET [UnitPrice]=CASE WHEN ISNUMERIC(Replace([UnitPrice],'$','')) = 1 THEN Replace([UnitPrice],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [UnitPrice]=CAST(CASE WHEN ISNUMERIC([UnitPrice] + 'e0') = 1 THEN [UnitPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [UnitPrice] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [AffiliateRecurringPrice]='0' WHERE [AffiliateRecurringPrice] IS NULL
GO
UPDATE [ShopProducts] SET [AffiliateRecurringPrice]=CASE WHEN ISNUMERIC(Replace([AffiliateRecurringPrice],'$','')) = 1 THEN Replace([AffiliateRecurringPrice],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [AffiliateRecurringPrice]=CAST(CASE WHEN ISNUMERIC([AffiliateRecurringPrice] + 'e0') = 1 THEN [AffiliateRecurringPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [AffiliateRecurringPrice] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [RecurringPrice]='0' WHERE [RecurringPrice] IS NULL
GO
UPDATE [ShopProducts] SET [RecurringPrice]=CASE WHEN ISNUMERIC(Replace([RecurringPrice],'$','')) = 1 THEN Replace([RecurringPrice],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [RecurringPrice]=CAST(CASE WHEN ISNUMERIC([RecurringPrice] + 'e0') = 1 THEN [RecurringPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [RecurringPrice] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [Handling]='0' WHERE [Handling] IS NULL
GO
UPDATE [ShopProducts] SET [Handling]=CASE WHEN ISNUMERIC(Replace([Handling],'$','')) = 1 THEN Replace([Handling],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [Handling]=CAST(CASE WHEN ISNUMERIC([Handling] + 'e0') = 1 THEN [Handling] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [Handling] [decimal] (18,0) NULL
GO

UPDATE [ShopProducts] SET [SalePrice]='0' WHERE [SalePrice] IS NULL
GO
UPDATE [ShopProducts] SET [SalePrice]=CASE WHEN ISNUMERIC(Replace([SalePrice],'$','')) = 1 THEN Replace([SalePrice],'$','') ELSE '0' END
GO
UPDATE [ShopProducts] SET [SalePrice]=CAST(CASE WHEN ISNUMERIC([SalePrice] + 'e0') = 1 THEN [SalePrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ShopProducts] ALTER COLUMN [SalePrice] [decimal] (18,0) NULL
GO

UPDATE [AffiliatePaid] SET [AmountPaid]='0' WHERE [AmountPaid] IS NULL
GO
UPDATE [AffiliatePaid] SET [AmountPaid]=CASE WHEN ISNUMERIC(Replace([AmountPaid],'$','')) = 1 THEN Replace([AmountPaid],'$','') ELSE '0' END
GO
UPDATE AffiliatePaid SET AmountPaid=CAST(CASE WHEN ISNUMERIC(AmountPaid + 'e0') = 1 THEN AmountPaid ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[AffiliatePaid] ALTER COLUMN [AmountPaid] [decimal] (18,0) NULL
GO

UPDATE [AuctionAds] SET [StartBid]='0' WHERE [StartBid] IS NULL
GO
UPDATE [AuctionAds] SET [StartBid]=CASE WHEN ISNUMERIC(Replace([StartBid],'$','')) = 1 THEN Replace([StartBid],'$','') ELSE '0' END
GO
UPDATE [AuctionAds] SET [StartBid]=CAST(CASE WHEN ISNUMERIC([StartBid] + 'e0') = 1 THEN [StartBid] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[AuctionAds] ALTER COLUMN [StartBid] [decimal] (18,0) NULL
GO

UPDATE [AuctionAds] SET [MaxBid]='0' WHERE [MaxBid] IS NULL
GO
UPDATE [AuctionAds] SET [MaxBid]=CASE WHEN ISNUMERIC(Replace([MaxBid],'$','')) = 1 THEN Replace([MaxBid],'$','') ELSE '0' END
GO
UPDATE [AuctionAds] SET [MaxBid]=CAST(CASE WHEN ISNUMERIC([MaxBid] + 'e0') = 1 THEN [MaxBid] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[AuctionAds] ALTER COLUMN [MaxBid] [decimal] (18,0) NULL
GO

UPDATE [AuctionAds] SET [CurrentBid]='0' WHERE [CurrentBid] IS NULL
GO
UPDATE [AuctionAds] SET [CurrentBid]=CASE WHEN ISNUMERIC(Replace([CurrentBid],'$','')) = 1 THEN Replace([CurrentBid],'$','') ELSE '0' END
GO
UPDATE [AuctionAds] SET [CurrentBid]=CAST(CASE WHEN ISNUMERIC([CurrentBid] + 'e0') = 1 THEN [CurrentBid] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[AuctionAds] ALTER COLUMN [CurrentBid] [decimal] (18,0) NULL
GO

UPDATE [ClassifiedsAds] SET [Price]='0' WHERE [Price] IS NULL
GO
UPDATE [ClassifiedsAds] SET [Price]=CASE WHEN ISNUMERIC(Replace([Price],'$','')) = 1 THEN Replace([Price],'$','') ELSE '0' END
GO
UPDATE [ClassifiedsAds] SET [Price]=CAST(CASE WHEN ISNUMERIC([Price] + 'e0') = 1 THEN [Price] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[ClassifiedsAds] ALTER COLUMN [Price] [decimal] (18,0) NULL
GO

UPDATE [AuctionAds] SET [BidIncrease]='0' WHERE [BidIncrease] IS NULL
GO
UPDATE [AuctionAds] SET [BidIncrease]=CASE WHEN ISNUMERIC(Replace([BidIncrease],'$','')) = 1 THEN Replace([BidIncrease],'$','') ELSE '0' END
GO
UPDATE [AuctionAds] SET [BidIncrease]=CAST(CASE WHEN ISNUMERIC([BidIncrease] + 'e0') = 1 THEN [BidIncrease] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[AuctionAds] ALTER COLUMN [BidIncrease] [decimal] (18,0) NULL
GO

UPDATE [CustomFieldOptions] SET [RecurringPrice]='0' WHERE [RecurringPrice] IS NULL
GO
UPDATE [CustomFieldOptions] SET [RecurringPrice]=CASE WHEN ISNUMERIC(Replace([RecurringPrice],'$','')) = 1 THEN Replace([RecurringPrice],'$','') ELSE '0' END
GO
UPDATE [CustomFieldOptions] SET [RecurringPrice]=CAST(CASE WHEN ISNUMERIC([RecurringPrice] + 'e0') = 1 THEN [RecurringPrice] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[CustomFieldOptions] ALTER COLUMN [RecurringPrice] [decimal] (18,0) NULL
GO

UPDATE [CustomFieldOptions] SET [Price]='0' WHERE [Price] IS NULL
GO
UPDATE [CustomFieldOptions] SET [Price]=CASE WHEN ISNUMERIC(Replace([Price],'$','')) = 1 THEN Replace([Price],'$','') ELSE '0' END
GO
UPDATE [CustomFieldOptions] SET [Price]=CAST(CASE WHEN ISNUMERIC([Price] + 'e0') = 1 THEN [Price] ELSE '0' END AS decimal(38, 10))
GO
ALTER TABLE [dbo].[CustomFieldOptions] ALTER COLUMN [Price] [decimal] (18,0) NULL
GO

/****** Object:  Index [PrivateClass]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PrivateClass] ON [dbo].[AccessClasses]
(
	[PrivateClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AccessClasses]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AccessKeys]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ActType]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ActType] ON [dbo].[Activities]
(
	[ActType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IPAddress]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [IPAddress] ON [dbo].[Activities]
(
	[IPAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Activities]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Activities]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Activities]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Activities]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Country]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Country] ON [dbo].[Advertisements]
(
	[Country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[Advertisements]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[Advertisements]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [State]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [State] ON [dbo].[Advertisements]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Advertisements]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Advertisements]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ZoneID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ZoneID] ON [dbo].[Advertisements]
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[AffiliatePaid]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [InvoiceID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [InvoiceID] ON [dbo].[AffiliatePaid]
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Alumni]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Alumni]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ApproveID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ApproveID] ON [dbo].[ApprovalEmails]
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ApprovalEmails]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Approvals]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ApproveID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ApproveID] ON [dbo].[ApprovalXML]
(
	[ApproveID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ApprovalXML]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ApprovalXML]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[ApprovalXML]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Articles]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [End_Date]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [End_Date] ON [dbo].[Articles]
(
	[End_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Articles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Start_Date]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Start_Date] ON [dbo].[Articles]
(
	[Start_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Articles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Articles]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Associations]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Associations]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Associations]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[AuctionAds]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[AuctionAds]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[AuctionAds]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[AuctionAds]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AuctionAds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[AuctionAds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AdID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AdID] ON [dbo].[AuctionFeedback]
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [BORS]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [BORS] ON [dbo].[AuctionFeedback]
(
	[BORS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[AuctionFeedback]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[AuctionFeedback]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[AuctionFeedback]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ProcessID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProcessID] ON [dbo].[BG_Emails]
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[BG_Processes]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ProcessID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProcessID] ON [dbo].[BG_SMS]
(
	[ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[Blog]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Blog]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[Blog]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Blog]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Blog]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[BusinessListings]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ClaimID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ClaimID] ON [dbo].[BusinessListings]
(
	[ClaimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[BusinessListings]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[BusinessListings]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[BusinessListings]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[BusinessListings]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExcPortalSecurity]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExcPortalSecurity] ON [dbo].[Categories]
(
	[ExcPortalSecurity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ListUnder]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ListUnder] ON [dbo].[Categories]
(
	[ListUnder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Sharing]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Sharing] ON [dbo].[Categories]
(
	[Sharing] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ShowList]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ShowList] ON [dbo].[Categories]
(
	[ShowList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Categories]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Categories]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesModules]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CategoriesModules]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CategoriesModules]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesPages]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CategoriesPages]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CategoriesPages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[CategoriesPortals]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CategoriesPortals]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ClassifiedsAds]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ClassifiedsAds]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[ClassifiedsAds]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ClassifiedsAds]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Soldout]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Soldout] ON [dbo].[ClassifiedsAds]
(
	[Soldout] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [SoldUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SoldUserID] ON [dbo].[ClassifiedsAds]
(
	[SoldUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ClassifiedsAds]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ClassifiedsAds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AdID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AdID] ON [dbo].[ClassifiedsFeedback]
(
	[AdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [BORS]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [BORS] ON [dbo].[ClassifiedsFeedback]
(
	[BORS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[ClassifiedsFeedback]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ClassifiedsFeedback]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[ClassifiedsFeedback]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Comments]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReplyID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ReplyID] ON [dbo].[Comments]
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Comments]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Comments]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CommentID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CommentID] ON [dbo].[CommentsLikes]
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CommentsLikes]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[CommentsLikes]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ContentRotator]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ZoneID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ZoneID] ON [dbo].[ContentRotator]
(
	[ZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [FieldID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FieldID] ON [dbo].[CustomFieldOptions]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomFieldOptions]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Searchable]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Searchable] ON [dbo].[CustomFields]
(
	[Searchable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SectionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SectionID] ON [dbo].[CustomFields]
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomFields]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomFields]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [FieldID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FieldID] ON [dbo].[CustomFieldUsers]
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[CustomFieldUsers]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[CustomFieldUsers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomFieldUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[CustomFieldUsers]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[CustomFieldUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[CustomSections]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[CustomSections]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[DiscountSystem]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [DiscountCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [DiscountCode] ON [dbo].[DiscountSystem]
(
	[DiscountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[DiscountSystem]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[DiscountSystem]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Quantity]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Quantity] ON [dbo].[DiscountSystem]
(
	[Quantity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ShowWeb]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ShowWeb] ON [dbo].[DiscountSystem]
(
	[ShowWeb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[DiscountSystem]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[DiscountSystem]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ELearnCourses]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ELearnCourses]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ELearnCourses]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[ELearnCourses]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnCourses]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[ELearnExamGrades]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamGrades]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnExamGrades]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamQuestions]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnExams]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[ELearnExams]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[ELearnExams]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnExams]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExamID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExamID] ON [dbo].[ELearnExamUsers]
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Graded]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Graded] ON [dbo].[ELearnExamUsers]
(
	[Graded] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[ELearnExamUsers]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnExamUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[ELearnHomeUsers]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [HomeID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [HomeID] ON [dbo].[ELearnHomeUsers]
(
	[HomeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnHomeUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnHomework]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [DueDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [DueDate] ON [dbo].[ELearnHomework]
(
	[DueDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnHomework]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Active]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Active] ON [dbo].[ELearnStudents]
(
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CourseID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CourseID] ON [dbo].[ELearnStudents]
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ELearnStudents]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ELearnStudents]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EmailTemplates]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EmailTemplates]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Duration]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Duration] ON [dbo].[EventCalendar]
(
	[Duration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EventDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EventDate] ON [dbo].[EventCalendar]
(
	[EventDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[EventCalendar]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EventCalendar]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Recurring]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Recurring] ON [dbo].[EventCalendar]
(
	[Recurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Shared]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Shared] ON [dbo].[EventCalendar]
(
	[Shared] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EventCalendar]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [TypeID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [TypeID] ON [dbo].[EventCalendar]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[EventCalendar]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[EventTypes]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[EventTypes]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[FAQ]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[FAQ]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FAQ]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FAQ]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Favorites]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Favorites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Feeds]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Feeds]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Feeds]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FeedID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FeedID] ON [dbo].[FeedsUsers]
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isDeleted]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isDeleted] ON [dbo].[FeedsUsers]
(
	[isDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [isFavorite]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isFavorite] ON [dbo].[FeedsUsers]
(
	[isFavorite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FeedsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormAnswers]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[FormAnswers]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormAnswers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SubmissionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SubmissionID] ON [dbo].[FormAnswers]
(
	[SubmissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FormAnswers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[FormOptions]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormQuestions]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SectionID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SectionID] ON [dbo].[FormQuestions]
(
	[SectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormQuestions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FormQuestions]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormReplyIDs]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Available]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Available] ON [dbo].[Forms]
(
	[Available] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Forms]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReplyFormID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ReplyFormID] ON [dbo].[Forms]
(
	[ReplyFormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Forms]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [FormID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FormID] ON [dbo].[FormSections]
(
	[FormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[FormSections]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[FormSections]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ForumsMessages]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EmailReply]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EmailReply] ON [dbo].[ForumsMessages]
(
	[EmailReply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ForumsMessages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReplyID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ReplyID] ON [dbo].[ForumsMessages]
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ForumsMessages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ForumsMessages]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [AddedUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AddedUserID] ON [dbo].[FriendsList]
(
	[AddedUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[FriendsList]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[FriendsList]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[GalleryPhotos]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[GalleryPhotos]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[GalleryPhotos]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[GalleryPhotos]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CommunityCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CommunityCode] ON [dbo].[Geo]
(
	[CommunityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CommunityName]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CommunityName] ON [dbo].[Geo]
(
	[CommunityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CountryCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CountryCode] ON [dbo].[Geo]
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Latitude]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Latitude] ON [dbo].[Geo]
(
	[Latitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Longitude]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Longitude] ON [dbo].[Geo]
(
	[Longitude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [PostalCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PostalCode] ON [dbo].[Geo]
(
	[PostalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ProvinceCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProvinceCode] ON [dbo].[Geo]
(
	[ProvinceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ProvinceName]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProvinceName] ON [dbo].[Geo]
(
	[ProvinceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RegionCode]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [RegionCode] ON [dbo].[Geo]
(
	[RegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RegionName]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [RegionName] ON [dbo].[Geo]
(
	[RegionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[GroupLists]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[GroupLists]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ListID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ListID] ON [dbo].[GroupListsUsers]
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[GroupListsUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[GroupListsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[Guestbook]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Guestbook]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Guestbook]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Guestbook]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[Invoices]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [inCart]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [inCart] ON [dbo].[Invoices]
(
	[inCart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [isRecurring]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isRecurring] ON [dbo].[Invoices]
(
	[isRecurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Invoices]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [RecurringID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [RecurringID] ON [dbo].[Invoices]
(
	[RecurringID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Invoices]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Invoices]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExcludeAffiliate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExcludeAffiliate] ON [dbo].[Invoices_Products]
(
	[ExcludeAffiliate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [InvoiceID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [InvoiceID] ON [dbo].[Invoices_Products]
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [isRecurring]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isRecurring] ON [dbo].[Invoices_Products]
(
	[isRecurring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LinkID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LinkID] ON [dbo].[Invoices_Products]
(
	[LinkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Invoices_Products]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Invoices_Products]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ProductID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProductID] ON [dbo].[Invoices_Products]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Invoices_Products]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[JobListCompanies]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[JobListCompanies]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[JobListCompanies]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CompanyID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CompanyID] ON [dbo].[JobListings]
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[JobListings]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[JobListings]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [TitleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [TitleID] ON [dbo].[JobListings]
(
	[TitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[JobListResumes]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[JobListResumes]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[JobListResumes]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Viewable]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Viewable] ON [dbo].[JobListResumes]
(
	[Viewable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[JobListTitles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[JobListTitles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[LibrariesFiles]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[LibrariesFiles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[LibrariesFiles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[LinksWebSites]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[LinksWebSites]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[LinksWebSites]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[LinksWebSites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndChat]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndChat] ON [dbo].[LiveChat]
(
	[EndChat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [OperatorID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [OperatorID] ON [dbo].[LiveChat]
(
	[OperatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Requesting]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Requesting] ON [dbo].[LiveChat]
(
	[Requesting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[LiveChat]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ChatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ChatID] ON [dbo].[LiveChatLogs]
(
	[ChatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[LiveChatLogs]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [isOnline]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isOnline] ON [dbo].[LiveChatUsers]
(
	[isOnline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[LiveChatUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[LiveChatUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[MatchMaker]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[MatchMaker]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[MatchMaker]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AffiliateID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AffiliateID] ON [dbo].[Members]
(
	[AffiliateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CustomerID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CustomerID] ON [dbo].[Members]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Facebook_Id]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Facebook_Id] ON [dbo].[Members]
(
	[Facebook_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Facebook_User]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Facebook_User] ON [dbo].[Members]
(
	[Facebook_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [HideTips]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [HideTips] ON [dbo].[Members]
(
	[HideTips] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PasswordResetID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PasswordResetID] ON [dbo].[Members]
(
	[PasswordResetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PCRCandidateId]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PCRCandidateId] ON [dbo].[Members]
(
	[PCRCandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PCRCompanyId]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PCRCompanyId] ON [dbo].[Members]
(
	[PCRCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Members]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReferralID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ReferralID] ON [dbo].[Members]
(
	[ReferralID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SiteID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SiteID] ON [dbo].[Members]
(
	[SiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Members]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [SugarID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SugarID] ON [dbo].[Members]
(
	[SugarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
CREATE INDEX [BannedIPs] ON [Activities]([IPAddress],[ActType],[ModuleID],[Status])
GO
CREATE INDEX [LastUpdated] ON [Scripts]([ScriptType],[DatePosted])
GO
CREATE INDEX [UpdateStats] ON [PageStats]([StatDate],[PortalID])
GO
CREATE INDEX [CustomPage] ON [ModulesNPages]([UniqueID],[PageID])
GO
CREATE INDEX [BGProgress] ON [BG_Processes]([ProcessName],[Status])
GO

/****** Object:  Index [UserName]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserName] ON [dbo].[Members]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [VTigerID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [VTigerID] ON [dbo].[Members]
(
	[VTigerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [VTigerLead]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [VTigerLead] ON [dbo].[Members]
(
	[VTigerLead] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[MembersKeywords]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[MembersKeywords]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[MembersKeywords]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[Messenger]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[Messenger]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[Messenger]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[MessengerBlocked]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[MessengerBlocked]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ExpireDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ExpireDate] ON [dbo].[MessengerSent]
(
	[ExpireDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromUserID] ON [dbo].[MessengerSent]
(
	[FromUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToUserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ToUserID] ON [dbo].[MessengerSent]
(
	[ToUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Activated]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Activated] ON [dbo].[ModulesNPages]
(
	[Activated] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [MenuID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [MenuID] ON [dbo].[ModulesNPages]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ModulesNPages]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PageID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PageID] ON [dbo].[ModulesNPages]
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ModulesNPages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[ModulesNPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[News]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[News]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[News]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[News]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Newsletters]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LetterID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LetterID] ON [dbo].[NewslettersSent]
(
	[LetterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[NewslettersSent]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[NewslettersSent]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [LetterID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [LetterID] ON [dbo].[NewslettersUsers]
(
	[LetterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[NewslettersUsers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[NewslettersUsers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[NewslettersUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [CurrentStatus]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CurrentStatus] ON [dbo].[OnlineUsers]
(
	[CurrentStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [isChatting]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [isChatting] ON [dbo].[OnlineUsers]
(
	[isChatting] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [RoomID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [RoomID] ON [dbo].[OnlineUsers]
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[OnlineUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PageStats]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StatDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StatDate] ON [dbo].[PageStats]
(
	[StatDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PhotoAlbums]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SharedAlbum]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [SharedAlbum] ON [dbo].[PhotoAlbums]
(
	[SharedAlbum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PhotoAlbums]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[PhotoAlbums]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PollID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PollID] ON [dbo].[PNQOptions]
(
	[PollID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PNQOptions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [EndDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [EndDate] ON [dbo].[PNQQuestions]
(
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [StartDate]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [StartDate] ON [dbo].[PNQQuestions]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PNQQuestions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [MenuID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [MenuID] ON [dbo].[PortalPages]
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PageID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PageID] ON [dbo].[PortalPages]
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PortalPages]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PortalPages]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ViewPage]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ViewPage] ON [dbo].[PortalPages]
(
	[ViewPage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[PortalPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Portals]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [DomainName]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [DomainName] ON [dbo].[Portals]
(
	[DomainName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [HideList]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [HideList] ON [dbo].[Portals]
(
	[HideList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Portals]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Portals]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ListUnder]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ListUnder] ON [dbo].[PortalScripts]
(
	[ListUnder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PortalScripts]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ScriptType]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ScriptType] ON [dbo].[PortalScripts]
(
	[ScriptType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[PostCards]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PostCards]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PostCards]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[PostCardsCat]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[PostCardsCat]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[PricingOptions]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[PricingOptions]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [HotOrNot]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [HotOrNot] ON [dbo].[Profiles]
(
	[HotOrNot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Profiles]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ProfileType]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ProfileType] ON [dbo].[Profiles]
(
	[ProfileType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Profiles]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Profiles]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Ratings]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Ratings]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Ratings]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[RecycleBin]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromEmailAddress]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromEmailAddress] ON [dbo].[ReferralAddresses]
(
	[FromEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ToEmailAddress]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ToEmailAddress] ON [dbo].[ReferralAddresses]
(
	[ToEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [FromEmailAddress]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [FromEmailAddress] ON [dbo].[ReferralStats]
(
	[FromEmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Reviews]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReviewID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ReviewID] ON [dbo].[ReviewsUsers]
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[ReviewsUsers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [BrokerID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [BrokerID] ON [dbo].[RStateAgents]
(
	[BrokerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateAgents]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateAgents]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateAgents]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Approval]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Approval] ON [dbo].[RStateBrokers]
(
	[Approval] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateBrokers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateBrokers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateBrokers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AgentID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [AgentID] ON [dbo].[RStateProperty]
(
	[AgentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ForSale]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ForSale] ON [dbo].[RStateProperty]
(
	[ForSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[RStateProperty]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PropertyType]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [PropertyType] ON [dbo].[RStateProperty]
(
	[PropertyType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[RStateProperty]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[RStateProperty]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [ScriptType]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [ScriptType] ON [dbo].[Scripts]
(
	[ScriptType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Scripts]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:26 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[ShopProducts]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[ShopProducts]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[ShopProducts]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[ShopProducts]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [enableUserPage]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [enableUserPage] ON [dbo].[SiteTemplates]
(
	[enableUserPage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SiteTemplates]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [useTemplate]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [useTemplate] ON [dbo].[SiteTemplates]
(
	[useTemplate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Speakers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Speakers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[SpeakTopics]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SpeakTopics]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [MenSport]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [MenSport] ON [dbo].[Sports]
(
	[MenSport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Sports]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Sports]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[SportsCoaches]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SportID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [SportID] ON [dbo].[SportsCoaches]
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SportsCoaches]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[SportsGames]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SportID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [SportID] ON [dbo].[SportsGames]
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SportsGames]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[SportsPlayers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [SportID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [SportID] ON [dbo].[SportsPlayers]
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[SportsPlayers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Stocks]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[TargetZones]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[TargetZones]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [State]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [State] ON [dbo].[TaxCalculator]
(
	[State] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[TaxCalculator]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesGuestbook]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Password]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Password] ON [dbo].[UPagesPages]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [TemplateID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [TemplateID] ON [dbo].[UPagesPages]
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesPages]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[UPagesPages]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [InviteOnly]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [InviteOnly] ON [dbo].[UPagesSites]
(
	[InviteOnly] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[UPagesSites]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ShowList]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ShowList] ON [dbo].[UPagesSites]
(
	[ShowList] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [TemplateID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [TemplateID] ON [dbo].[UPagesSites]
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UPagesSites]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Approved]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Approved] ON [dbo].[Uploads]
(
	[Approved] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [isTemp]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [isTemp] ON [dbo].[Uploads]
(
	[isTemp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ModuleID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ModuleID] ON [dbo].[Uploads]
(
	[ModuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Uploads]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [UniqueID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UniqueID] ON [dbo].[Uploads]
(
	[UniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Uploads]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Weight]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Uploads]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[UserReviewAnswers]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[UserReviewAnswers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[UserReviewAnswers]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ReviewID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ReviewID] ON [dbo].[UserReviewAnswers]
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[UserReviewAnswers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[UserReviewAnswers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[UserReviewCustomOptions]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [QuestionID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [QuestionID] ON [dbo].[UserReviewCustomOptions]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[UserReviewCustomOptions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[UserReviewQuestions]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[UserReviewQuestions]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[UserReviewQuestions]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [AdminEmailID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [AdminEmailID] ON [dbo].[Vouchers]
(
	[AdminEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [ApproveEmailID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [ApproveEmailID] ON [dbo].[Vouchers]
(
	[ApproveEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [BuyEmailID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [BuyEmailID] ON [dbo].[Vouchers]
(
	[BuyEmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CatID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [CatID] ON [dbo].[Vouchers]
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[Vouchers]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [RedemptionEnd]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [RedemptionEnd] ON [dbo].[Vouchers]
(
	[RedemptionEnd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [RedemptionStart]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [RedemptionStart] ON [dbo].[Vouchers]
(
	[RedemptionStart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Status]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Status] ON [dbo].[Vouchers]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[Vouchers]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [CartID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [CartID] ON [dbo].[VouchersPurchased]
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [PortalID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [PortalID] ON [dbo].[VouchersPurchased]
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [Redeemed]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [Redeemed] ON [dbo].[VouchersPurchased]
(
	[Redeemed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [UserID] ON [dbo].[VouchersPurchased]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
/****** Object:  Index [VoucherID]    Script Date: 11/11/2015 8:58:27 PM ******/
CREATE NONCLUSTERED INDEX [VoucherID] ON [dbo].[VouchersPurchased]
(
	[VoucherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO

UPDATE Members SET UserID=NEWID() WHERE UserID IS NULL
GO

ALTER TABLE Members ALTER COLUMN UserID varchar (40) NOT NULL
GO

ALTER TABLE [dbo].[AccessKeys] 
ADD CONSTRAINT [PK_KeyID_AccessKeys] PRIMARY KEY CLUSTERED (KeyID);
GO

ALTER TABLE [dbo].[Activities] 
ADD CONSTRAINT [pk_ActivityID_Activities] PRIMARY KEY CLUSTERED (ActivityID);
GO
 
ALTER TABLE [dbo].[Advertisements] 
ADD CONSTRAINT [PK_Advertisements_1_10] PRIMARY KEY CLUSTERED (AdID);
GO
 
ALTER TABLE [dbo].[AffiliatePaid] 
ADD CONSTRAINT [PK_PaidID_1_10] PRIMARY KEY CLUSTERED (PaidID);
GO
 
ALTER TABLE [dbo].[Alumni] 
ADD CONSTRAINT [PK_Alumni_1_10] PRIMARY KEY CLUSTERED (AlumniID);
GO
 
ALTER TABLE [dbo].[ApprovalEmails] 
ADD CONSTRAINT [PK_EmailID_1_10] PRIMARY KEY CLUSTERED (EmailID);
GO
 
ALTER TABLE [dbo].[Approvals] 
ADD CONSTRAINT [PK_ApproveID_1_10] PRIMARY KEY CLUSTERED (ApproveID);
GO
 
ALTER TABLE [dbo].[ApprovalXML] 
ADD CONSTRAINT [PK_XMLID_1_10] PRIMARY KEY CLUSTERED (XMLID);
GO
 
ALTER TABLE [dbo].[Articles] 
ADD CONSTRAINT [PK_Articles_1_10] PRIMARY KEY CLUSTERED (ArticleID);
GO
 
ALTER TABLE [dbo].[Associations] 
ADD CONSTRAINT [PK_AssociationID_1_10] PRIMARY KEY CLUSTERED (AssociationID);
GO
 
ALTER TABLE [dbo].[AuctionAds] 
ADD CONSTRAINT [PK_AuctionAds_1_10] PRIMARY KEY CLUSTERED (AdID);
GO
 
ALTER TABLE [dbo].[AuctionFeedback] 
ADD CONSTRAINT [pk_FeedbackID_AuctionFeedback] PRIMARY KEY CLUSTERED (FeedbackID);
GO
 
ALTER TABLE [dbo].[BG_Processes] 
ADD CONSTRAINT [pk_ProcessID_BG_Processes] PRIMARY KEY CLUSTERED (ProcessID);
GO
 
ALTER TABLE [dbo].[Blog] 
ADD CONSTRAINT [PK_Blog_1_10] PRIMARY KEY CLUSTERED (BlogID);
GO
 
ALTER TABLE [dbo].[BusinessListings] 
ADD CONSTRAINT [PK_BusinessID_1_10] PRIMARY KEY CLUSTERED (BusinessID);
GO
 
ALTER TABLE [dbo].[Categories] 
ADD CONSTRAINT [PK_CatID_Categories] PRIMARY KEY CLUSTERED (CatID);
GO
 
ALTER TABLE [dbo].[CategoriesModules] 
ADD CONSTRAINT [PK_UniqueID_8_10] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[CategoriesPages] 
ADD CONSTRAINT [PK_UniqueID_7_10] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[CategoriesPortals] 
ADD CONSTRAINT [PK_UniqueID_9_10] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[ClassifiedsAds] 
ADD CONSTRAINT [PK_AdID_1_10] PRIMARY KEY CLUSTERED (AdID);
GO
 
ALTER TABLE [dbo].[ClassifiedsFeedback] 
ADD CONSTRAINT [PK_FeedbackID_1_10] PRIMARY KEY CLUSTERED (FeedbackID);
GO
 
ALTER TABLE [dbo].[Comments] 
ADD CONSTRAINT [PK_CommentID_Comments] PRIMARY KEY CLUSTERED (CommentID);
GO
 
ALTER TABLE [dbo].[CommentsLikes] 
ADD CONSTRAINT [pk_LikeID_CommentsLikes] PRIMARY KEY CLUSTERED (LikeID);
GO
 
ALTER TABLE [dbo].[ContentRotator] 
ADD CONSTRAINT [PK_ContentRotator_1_10] PRIMARY KEY CLUSTERED (ContentID);
GO
 
ALTER TABLE [dbo].[CustomFieldOptions] 
ADD CONSTRAINT [PK_OptionID_4_10] PRIMARY KEY CLUSTERED (OptionID);
GO
 
ALTER TABLE [dbo].[CustomFields] 
ADD CONSTRAINT [PK_FieldID_3_10] PRIMARY KEY CLUSTERED (FieldID);
GO
 
ALTER TABLE [dbo].[CustomFieldUsers] 
ADD CONSTRAINT [PK_UserFieldID_3_10] PRIMARY KEY CLUSTERED (UserFieldID);
GO
 
ALTER TABLE [dbo].[CustomSections] 
ADD CONSTRAINT [pk_SectionID_CustomSections] PRIMARY KEY CLUSTERED (SectionID);
GO
 
ALTER TABLE [dbo].[DiscountSystem] 
ADD CONSTRAINT [PK_DiscountID_1_10] PRIMARY KEY CLUSTERED (DiscountID);
GO
 
ALTER TABLE [dbo].[ELearnCourses] 
ADD CONSTRAINT [PK_CourseID_1_10] PRIMARY KEY CLUSTERED (CourseID);
GO
 
ALTER TABLE [dbo].[ELearnExamGrades] 
ADD CONSTRAINT [PK_GradeID_1_10] PRIMARY KEY CLUSTERED (GradeID);
GO
 
ALTER TABLE [dbo].[ELearnExamQuestions] 
ADD CONSTRAINT [PK_QuestionID_1_10] PRIMARY KEY CLUSTERED (QuestionID);
GO
 
ALTER TABLE [dbo].[ELearnExams] 
ADD CONSTRAINT [PK_ExamID_1_10] PRIMARY KEY CLUSTERED (ExamID);
GO
 
ALTER TABLE [dbo].[ELearnExamUsers] 
ADD CONSTRAINT [PK_EUserID_1_10] PRIMARY KEY CLUSTERED (EUserID);
GO
 
ALTER TABLE [dbo].[ELearnHomeUsers] 
ADD CONSTRAINT [PK_HUserID_1_10] PRIMARY KEY CLUSTERED (HUserID);
GO
 
ALTER TABLE [dbo].[ELearnHomework] 
ADD CONSTRAINT [PK_HomeID_1_10] PRIMARY KEY CLUSTERED (HomeID);
GO
 
ALTER TABLE [dbo].[ELearnStudents] 
ADD CONSTRAINT [PK_StudentID_1_10] PRIMARY KEY CLUSTERED (StudentID);
GO
 
ALTER TABLE [dbo].[EmailTemplates] 
ADD CONSTRAINT [pk_TemplateID_EmailTemplates] PRIMARY KEY CLUSTERED (TemplateID);
GO
 
ALTER TABLE [dbo].[EventCalendar] 
ADD CONSTRAINT [PK_EventID_1_10] PRIMARY KEY CLUSTERED (EventID);
GO
 
ALTER TABLE [dbo].[EventTypes] 
ADD CONSTRAINT [PK_TypeID_1_10] PRIMARY KEY CLUSTERED (TypeID);
GO
 
ALTER TABLE [dbo].[FAQ] 
ADD CONSTRAINT [PK_FAQID_1_10] PRIMARY KEY CLUSTERED (FAQID);
GO
 
ALTER TABLE [dbo].[Favorites] 
ADD CONSTRAINT [PK_ID_1_30] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[Feeds] 
ADD CONSTRAINT [PK_FeedID_Feeds] PRIMARY KEY CLUSTERED (FeedID);
GO
 
ALTER TABLE [dbo].[FormAnswers] 
ADD CONSTRAINT [PK_AnswerID_1_10] PRIMARY KEY CLUSTERED (AnswerID);
GO
 
ALTER TABLE [dbo].[FormOptions] 
ADD CONSTRAINT [PK_OptionID_1_20] PRIMARY KEY CLUSTERED (OptionID);
GO
 
ALTER TABLE [dbo].[FormQuestions] 
ADD CONSTRAINT [PK_QuestionID_1_20] PRIMARY KEY CLUSTERED (QuestionID);
GO
 
ALTER TABLE [dbo].[FormReplyIDs] 
ADD CONSTRAINT [pk_ReplyID_FormReplyIDs] PRIMARY KEY CLUSTERED (ReplyID);
GO
 
ALTER TABLE [dbo].[Forms] 
ADD CONSTRAINT [PK_Forms_1_10] PRIMARY KEY CLUSTERED (FormID);
GO
 
ALTER TABLE [dbo].[FormSections] 
ADD CONSTRAINT [PK_SectionID_1_10] PRIMARY KEY CLUSTERED (SectionID);
GO
 
ALTER TABLE [dbo].[ForumsMessages] 
ADD CONSTRAINT [PK_TopicID_1_10] PRIMARY KEY CLUSTERED (TopicID);
GO
 
ALTER TABLE [dbo].[ForumsPolls] 
ADD CONSTRAINT [pk_PollID_ForumsPolls] PRIMARY KEY CLUSTERED (PollID);
GO
 
ALTER TABLE [dbo].[FriendsList] 
ADD CONSTRAINT [pk_ID_FriendsList] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[GalleryPhotos] 
ADD CONSTRAINT [PK_PhotoID3_1_10] PRIMARY KEY CLUSTERED (PhotoID);
GO
 
ALTER TABLE [dbo].[Games] 
ADD CONSTRAINT [PK_GameID_1_10] PRIMARY KEY CLUSTERED (GameID);
GO
 
ALTER TABLE [dbo].[GroupLists] 
ADD CONSTRAINT [pk_ListID_GroupLists] PRIMARY KEY CLUSTERED (ListID);
GO
 
ALTER TABLE [dbo].[GroupListsUsers] 
ADD CONSTRAINT [pk_ListUserID_GroupListsUsers] PRIMARY KEY CLUSTERED (ListUserID);
GO
 
ALTER TABLE [dbo].[Guestbook] 
ADD CONSTRAINT [PK_EntryID_1_10] PRIMARY KEY CLUSTERED (EntryID);
GO
 
ALTER TABLE [dbo].[Invoices] 
ADD CONSTRAINT [PK_InvoiceID_1_10] PRIMARY KEY CLUSTERED (InvoiceID);
GO
 
ALTER TABLE [dbo].[Invoices_Products] 
ADD CONSTRAINT [PK_InvoiceProductID_1_10] PRIMARY KEY CLUSTERED (InvoiceProductID);
GO
 
ALTER TABLE [dbo].[JobListCompanies] 
ADD CONSTRAINT [PK_CompanyID_1_10] PRIMARY KEY CLUSTERED (CompanyID);
GO
 
ALTER TABLE [dbo].[JobListings] 
ADD CONSTRAINT [PK_JobID_1_10] PRIMARY KEY CLUSTERED (JobID);
GO
 
ALTER TABLE [dbo].[JobListResumes] 
ADD CONSTRAINT [PK_ResumeID_1_10] PRIMARY KEY CLUSTERED (ResumeID);
GO
 
ALTER TABLE [dbo].[JobListTitles] 
ADD CONSTRAINT [PK_TitleID_1_10] PRIMARY KEY CLUSTERED (TitleID);
GO
 
ALTER TABLE [dbo].[LibrariesFiles] 
ADD CONSTRAINT [PK_FileID_1_10] PRIMARY KEY CLUSTERED (FileID);
GO
 
ALTER TABLE [dbo].[LinksWebSites] 
ADD CONSTRAINT [PK_LinkID_1_10] PRIMARY KEY CLUSTERED (LinkID);
GO
 
ALTER TABLE [dbo].[LiveChat] 
ADD CONSTRAINT [PK_ChatID_1_10] PRIMARY KEY CLUSTERED (ChatID);
GO
 
ALTER TABLE [dbo].[LiveChatLogs] 
ADD CONSTRAINT [PK_LogID_1_10] PRIMARY KEY CLUSTERED (LogID);
GO
 
ALTER TABLE [dbo].[LocStates] 
ADD CONSTRAINT [PK_LocStates] PRIMARY KEY CLUSTERED (State_Id);
GO
 
ALTER TABLE [dbo].[MatchMaker] 
ADD CONSTRAINT [PK_ProfileID_1_10] PRIMARY KEY CLUSTERED (ProfileID);
GO
 
ALTER TABLE [dbo].[Members] 
ADD CONSTRAINT [PK_UserID_1_10] PRIMARY KEY CLUSTERED (UserID);
GO
 
ALTER TABLE [dbo].[MembersInvite] 
ADD CONSTRAINT [pk_InviteID_MembersInvite] PRIMARY KEY CLUSTERED (InviteID);
GO
 
ALTER TABLE [dbo].[MembersKeywords] 
ADD CONSTRAINT [pk_KeywordID_MembersKeywords] PRIMARY KEY CLUSTERED (KeywordID);
GO
 
ALTER TABLE [dbo].[Messenger] 
ADD CONSTRAINT [pk_ID_Messenger] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[MessengerBlocked] 
ADD CONSTRAINT [pk_ID_MessengerBlocked] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[MessengerSent] 
ADD CONSTRAINT [pk_ID_MessengerSent] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[ModulesNPages] 
ADD CONSTRAINT [PK_UniqueID_ModulesNPages] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[News] 
ADD CONSTRAINT [PK_NewsID_1_10] PRIMARY KEY CLUSTERED (NewsID);
GO
 
ALTER TABLE [dbo].[Newsletters] 
ADD CONSTRAINT [PK_LetterID_1_10] PRIMARY KEY CLUSTERED (LetterID);
GO
 
ALTER TABLE [dbo].[NewslettersSent] 
ADD CONSTRAINT [PK_SentID_1_10] PRIMARY KEY CLUSTERED (SentID);
GO
 
ALTER TABLE [dbo].[NewslettersUsers] 
ADD CONSTRAINT [PK_NUserID_1_10] PRIMARY KEY CLUSTERED (NUserID);
GO
 
ALTER TABLE [dbo].[OnlineUsers] 
ADD CONSTRAINT [pk_ID_OnlineUsers] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[PageStats] 
ADD CONSTRAINT [pk_ID_PageStats] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[PhotoAlbums] 
ADD CONSTRAINT [PK_AlbumID_1_10] PRIMARY KEY CLUSTERED (AlbumID);
GO
 
ALTER TABLE [dbo].[PNQOptions] 
ADD CONSTRAINT [PK_OptionID_1_10] PRIMARY KEY CLUSTERED (OptionID);
GO
 
ALTER TABLE [dbo].[PNQQuestions] 
ADD CONSTRAINT [PK_PollID_1_10] PRIMARY KEY CLUSTERED (PollID);
GO
 
ALTER TABLE [dbo].[PortalPages] 
ADD CONSTRAINT [PK_UniqueID_PortalPages] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[Portals] 
ADD CONSTRAINT [pk_PortalID_Portals] PRIMARY KEY CLUSTERED (PortalID);
GO
 
ALTER TABLE [dbo].[PortalScripts] 
ADD CONSTRAINT [pk_ID_PortalScripts] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[PostCards] 
ADD CONSTRAINT [PK_CardID_1_10] PRIMARY KEY CLUSTERED (CardID);
GO
 
ALTER TABLE [dbo].[PostCardsCat] 
ADD CONSTRAINT [PK_CatID_1_10] PRIMARY KEY CLUSTERED (CatID);
GO
 
ALTER TABLE [dbo].[PricingOptions] 
ADD CONSTRAINT [PK_PriceID_1_10] PRIMARY KEY CLUSTERED (PriceID);
GO
 
ALTER TABLE [dbo].[Profiles] 
ADD CONSTRAINT [PK_ProfileID_2_10] PRIMARY KEY CLUSTERED (ProfileID);
GO
 
ALTER TABLE [dbo].[Ratings] 
ADD CONSTRAINT [pk_ID_Ratings] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[RecycleBin] 
ADD CONSTRAINT [pk_UniqueID_RecycleBin] PRIMARY KEY CLUSTERED (UniqueID);
GO
 
ALTER TABLE [dbo].[ReferralAddresses] 
ADD CONSTRAINT [pk_ID_ReferralAddresses] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[ReferralStats] 
ADD CONSTRAINT [pk_ID_ReferralStats] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[Reviews] 
ADD CONSTRAINT [PK_ReviewID_Reviews] PRIMARY KEY CLUSTERED (ReviewID);
GO
 
ALTER TABLE [dbo].[ReviewsUsers] 
ADD CONSTRAINT [pk_ID_ReviewsUsers] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[RStateAgents] 
ADD CONSTRAINT [PK_AgentID_1_10] PRIMARY KEY CLUSTERED (AgentID);
GO
 
ALTER TABLE [dbo].[RStateBrokers] 
ADD CONSTRAINT [PK_BrokerID_1_10] PRIMARY KEY CLUSTERED (BrokerID);
GO
 
ALTER TABLE [dbo].[RStateProperty] 
ADD CONSTRAINT [PK_PropertyID_1_10] PRIMARY KEY CLUSTERED (PropertyID);
GO
 
ALTER TABLE [dbo].[Scripts] 
ADD CONSTRAINT [pk_ID_Scripts] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[SEOTitles] 
ADD CONSTRAINT [PK_TagID_SEOTitles] PRIMARY KEY CLUSTERED (TagID);
GO
 
ALTER TABLE [dbo].[ShopProducts] 
ADD CONSTRAINT [PK_ProductID_2_10] PRIMARY KEY CLUSTERED (ProductID);
GO
 
ALTER TABLE [dbo].[SiteTemplates] 
ADD CONSTRAINT [pk_TemplateID_SiteTemplates] PRIMARY KEY CLUSTERED (TemplateID);
GO
 
ALTER TABLE [dbo].[Speakers] 
ADD CONSTRAINT [PK_SpeakerID_1_10] PRIMARY KEY CLUSTERED (SpeakerID);
GO
 
ALTER TABLE [dbo].[SpeakSpeeches] 
ADD CONSTRAINT [PK_SpeechID_1_10] PRIMARY KEY CLUSTERED (SpeechID);
GO
 
ALTER TABLE [dbo].[SpeakTopics] 
ADD CONSTRAINT [PK_TopicID_2_10] PRIMARY KEY CLUSTERED (TopicID);
GO
 
ALTER TABLE [dbo].[Sports] 
ADD CONSTRAINT [PK_SportID_1_10] PRIMARY KEY CLUSTERED (SportID);
GO
 
ALTER TABLE [dbo].[SportsCoaches] 
ADD CONSTRAINT [PK_CoachID_1_10] PRIMARY KEY CLUSTERED (CoachID);
GO
 
ALTER TABLE [dbo].[SportsGames] 
ADD CONSTRAINT [PK_GameID_2_10] PRIMARY KEY CLUSTERED (GameID);
GO
 
ALTER TABLE [dbo].[SportsPlayers] 
ADD CONSTRAINT [PK_AthleteID_1_10] PRIMARY KEY CLUSTERED (AthleteID);
GO
 
ALTER TABLE [dbo].[Stocks] 
ADD CONSTRAINT [pk_ID_Stocks] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[TargetZones] 
ADD CONSTRAINT [PK_ZoneID_TargetZones] PRIMARY KEY CLUSTERED (ZoneID);
GO
 
ALTER TABLE [dbo].[TaxCalculator] 
ADD CONSTRAINT [pk_ID_TaxCalculator] PRIMARY KEY CLUSTERED (ID);
GO
 
ALTER TABLE [dbo].[UPagesGuestbook] 
ADD CONSTRAINT [PK_EntryID_2_10] PRIMARY KEY CLUSTERED (EntryID);
GO
 
ALTER TABLE [dbo].[UPagesPages] 
ADD CONSTRAINT [PK_PageID_1_10] PRIMARY KEY CLUSTERED (PageID);
GO
 
ALTER TABLE [dbo].[UPagesSites] 
ADD CONSTRAINT [PK_SiteID_1_10] PRIMARY KEY CLUSTERED (SiteID);
GO
 
ALTER TABLE [dbo].[Uploads] 
ADD CONSTRAINT [PK_UploadID_1_10] PRIMARY KEY CLUSTERED (UploadID);
GO
 
ALTER TABLE [dbo].[UserReviewAnswers] 
ADD CONSTRAINT [PK_AnswerID_2_10] PRIMARY KEY CLUSTERED (AnswerID);
GO
 
ALTER TABLE [dbo].[UserReviewCustomOptions] 
ADD CONSTRAINT [PK_OptionID_3_10] PRIMARY KEY CLUSTERED (OptionID);
GO
 
ALTER TABLE [dbo].[UserReviewQuestions] 
ADD CONSTRAINT [PK_QuestionID_2_10] PRIMARY KEY CLUSTERED (QuestionID);
GO
 
ALTER TABLE [dbo].[Vouchers] 
ADD CONSTRAINT [PK_Vouchers_1_10] PRIMARY KEY CLUSTERED (VoucherID);
GO
 
ALTER TABLE [dbo].[VouchersPurchased] 
ADD CONSTRAINT [PK_VouchersPurchased_1_10] PRIMARY KEY CLUSTERED (PurchaseID);
GO
ALTER TABLE Uploads ADD Weight [int]
GO
CREATE NONCLUSTERED INDEX [Weight] ON [dbo].[Uploads]
(
	[Weight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
GO
ALTER TABLE SiteTemplates ADD AccessKeys varchar(max)
GO
CREATE INDEX [PageLookup1] ON [PortalPages]([PortalID],[UniqueID],[Status])
GO
CREATE INDEX [PageLookup] ON [CategoriesPages]([ModuleID],[CatID],[PortalID])
GO
CREATE INDEX [PageLookup2] ON [PortalPages]([PageID],[PortalID],[Status])
GO
CREATE INDEX [Status] ON [UPagesSites]([Status])
GO
CREATE INDEX [LookupSite] ON [UPagesSites]([UserID],[Status])
GO
CREATE INDEX [LookupPage] ON [UPagesPages]([PageName],[UserID])
GO
