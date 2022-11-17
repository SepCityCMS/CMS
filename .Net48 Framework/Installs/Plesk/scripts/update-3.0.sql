
ALTER TABLE ShopProducts ALTER COLUMN [UnitPrice] decimal(18,4)
GO
ALTER TABLE ShopProducts ALTER COLUMN [SalePrice] decimal(18,4)
GO
ALTER TABLE ShopProducts ALTER COLUMN [RecurringPrice] decimal(18,4)
GO
ALTER TABLE ShopProducts ALTER COLUMN [Handling] decimal(18,4)
GO
ALTER TABLE ShopProducts ALTER COLUMN [AffiliateUnitPrice] decimal(18,4)
GO
ALTER TABLE ShopProducts ALTER COLUMN [AffiliateRecurringPrice] decimal(18,4)
GO
ALTER TABLE [AffiliatePaid] ALTER COLUMN [AmountPaid] decimal(18,4)
GO
ALTER TABLE [AuctionAds] ALTER COLUMN [StartBid] decimal(18,4)
GO
ALTER TABLE [AuctionAds] ALTER COLUMN [CurrentBid] decimal(18,4)
GO
ALTER TABLE [AuctionAds] ALTER COLUMN [MaxBid] decimal(18,4)
GO
ALTER TABLE [AuctionAds] ALTER COLUMN [BidIncrease] decimal(18,4)
GO
ALTER TABLE [ClassifiedsAds] ALTER COLUMN [Price] decimal(18,4)
GO
ALTER TABLE [CustomFieldOptions] ALTER COLUMN [Price] decimal(18,4)
GO
ALTER TABLE [CustomFieldOptions] ALTER COLUMN [RecurringPrice] decimal(18,4)
GO
ALTER TABLE [EventCalendar] ALTER COLUMN [EventOnlinePrice] decimal(18,4)
GO
ALTER TABLE [EventCalendar] ALTER COLUMN [EventDoorPrice] decimal(18,4)
GO
ALTER TABLE [Invoices_Products] ALTER COLUMN [UnitPrice] decimal(18,4)
GO
ALTER TABLE [Invoices_Products] ALTER COLUMN [RecurringPrice] decimal(18,4)
GO
ALTER TABLE [Invoices_Products] ALTER COLUMN [Handling] decimal(18,4)
GO
ALTER TABLE [Invoices_Products] ALTER COLUMN [AffiliateUnitPrice] decimal(18,4)
GO
ALTER TABLE [Invoices_Products] ALTER COLUMN [AffiliateRecurringPrice] decimal(18,4)
GO
insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('928000182738400', '70', '0', 'Radius and IP2Location', '', '', '0', '', '1', '', '', '', '', '0', '100')
GO
CREATE INDEX [UserIDStatus] ON [Activities]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Advertisements]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Articles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [AuctionAds]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Blog]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [BusinessListings]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ClassifiedsAds]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [CustomFieldUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [DiscountSystem]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ELearnStudents]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [EventCalendar]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [FormAnswers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ForumsMessages]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [GroupLists]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [GroupListsUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Guestbook]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Invoices]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [LibrariesFiles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [LinksWebSites]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [MatchMaker]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Members]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [NewslettersUsers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [PhotoAlbums]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [PNQQuestions]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Portals]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Profiles]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateAgents]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateBrokers]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [RStateProperty]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ShopProducts]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [ShopStores]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [UPagesSites]([UserID],[Status])
GO
CREATE INDEX [UserIDStatus] ON [Vouchers]([UserID],[Status])
GO
CREATE INDEX [UserNameStatus] ON [Members]([UserName],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Articles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [AuctionAds]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Blog]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [BusinessListings]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ClassifiedsAds]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [CustomFieldUsers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [DiscountSystem]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ELearnCourses]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EmailTemplates]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EventCalendar]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [EventTypes]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [FAQ]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Forms]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ForumsMessages]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Guestbook]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Invoices]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Invoices_Products]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [LibrariesFiles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [LinksWebSites]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [MatchMaker]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Members]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [News]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [NewslettersSent]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [NewslettersUsers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [PhotoAlbums]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [PortalPages]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Portals]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Profiles]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateAgents]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateBrokers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [RStateProperty]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopProducts]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopShippingMethods]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [ShopStores]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Speakers]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [SpeakSpeeches]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [SpeakTopics]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [UPagesSites]([PortalID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Vouchers]([PortalID],[Status])
GO
DROP Index Portals.Status
GO
DROP INDEX Portals.PortalIDStatus
GO
DROP INDEX Portals.UserIDStatus
GO
ALTER TABLE Portals ALTER COLUMN [Status] int
GO
CREATE INDEX [Status] ON [Portals]([Status])
GO
CREATE INDEX [UserIDStatus] ON [Portals]([UserID],[Status])
GO
CREATE INDEX [PortalIDStatus] ON [Portals]([PortalID],[Status])
GO
create nonclustered index [PageSelection] on PortalPages ([MenuID],[Status]) INCLUDE ([PageID],[PortalIDs])
GO
create nonclustered index [EventSelection] on EventCalendar ([PortalID],[Status]) INCLUDE ([UserID],[EventDate],[Shared])
GO
create nonclustered index [MembersReferral] on Members ([Status]) INCLUDE ([ReferralID])
GO
CREATE INDEX [CategoriesListUnderStatus] ON [Categories] ([ListUnder],[Status])
GO
CREATE INDEX [MembersStatusCountry] ON [Members] ([Status]) INCLUDE ([Country]);
GO
CREATE INDEX [MembersStatusPortalIDCreateDate] ON [Members] ([Status], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersMaleStatusPortalID] ON [Members] ([Male], [Status], [PortalID])
GO
CREATE INDEX [MembersMaleStatusPortalID_reateDate] ON [Members] ([Male], [Status], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersMalePortalIDCreateDate] ON [Members] ([Male], [PortalID],[CreateDate])
GO
CREATE INDEX [MembersStatusPortalIDCountry] ON [Members] ([Status], [PortalID],[Country])
GO
CREATE INDEX [MembersStatusHideTips] ON [Members] ([Status]) INCLUDE ([HideTips]);
GO
CREATE INDEX [MembersStatusCreateDate] ON [Members] ([Status],[CreateDate])
GO

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('495401920019923', '69', '69', 'Video Conference', '<h1>Video Conference</h1>', '', '3', '', '1', 'conference.aspx', '', '', '', '1', '100')
GO

/****** Object:  Table [dbo].[VideoConfig]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].VideoConfig(
	[ContactOnline] [bit] NULL,
	[SundayAvailableStart] [varchar] (10) NULL,
	[SundayAvailableEnd] [varchar] (10) NULL,
	[MondayAvailableStart] [varchar] (10) NULL,
	[MondayAvailableEnd] [varchar] (10) NULL,
	[TuesdayAvailableStart] [varchar] (10) NULL,
	[TuesdayAvailableEnd] [varchar] (10) NULL,
	[WednesdayAvailableStart] [varchar] (10) NULL,
	[WednesdayAvailableEnd] [varchar] (10) NULL,
	[ThursdayAvailableStart] [varchar] (10) NULL,
	[ThursdayAvailableEnd] [varchar] (10) NULL,
	[FridayAvailableStart] [varchar] (10) NULL,
	[FridayAvailableEnd] [varchar] (10) NULL,
	[SaturdayAvailableStart] [varchar] (10) NULL,
	[SaturdayAvailableEnd] [varchar] (10) NULL,
	[UserID] [varchar] (40) NOT NULL,
 CONSTRAINT [PK_UserID_VideoConfig] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[VideoSchedule]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].VideoSchedule(
	[MeetingID] [float] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Subject] [nvarchar] (100) NOT NULL,
	[Message] [ntext] NULL,
	[Accepted] [bit] NULL,
	[DateAccepted] [datetime] NULL,
	[Notes] [ntext] NULL,
	[FromUserID] [varchar] (40) NOT NULL,
	[UserID] [varchar] (40) NOT NULL,
 CONSTRAINT [PK_UserID_Schedule] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[APICalls]    Script Date: 5/21/2020 9:43:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].APICalls(
	[APIID] [float] NOT NULL,
	[Method] [varchar] (20) NOT NULL,
	[ApiURL] [varchar] (2048) NOT NULL,
	[ApiHeaders] [ntext] NULL,
	[ApiBody] [ntext] NULL,
	[Status] [int] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_APIID_APICalls] PRIMARY KEY CLUSTERED 
(
	[APIID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into ModulesNPages (UniqueID, ModuleID, PageID, LinkText, PageText, Description, MenuID, Keywords, Status, UserPageName, AdminPageName, AccessKeys, EditKeys, Activated, Weight) VALUES('493866645768917', '988', '0', 'API Calls', '', '', '0', '', '1', '', '', '', '', '1', '100')
GO

ALTER TABLE ShopProducts ADD [APIID] [float]
GO
